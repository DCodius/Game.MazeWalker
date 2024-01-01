using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private enum PlayerState
	{
		Idle,
		Walk,
		Run,
		Jump,
		Hurt,
	}


	[Export]
	public const float Speed = 300.0f;
	[Export]
	public const float JumpVelocity = -400.0f;
	[Export]
	public const float WalkDistance = 1.0f;
	public Vector2 ScreenSize; // Size of the game window.
	private Vector2 _velocity ;
	private Vector2 _position ;
	private float _curSpeed = Speed;
	private CollisionShape2D _collisionShape;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private AnimationPlayer _anim;
	private PlayerState _state = PlayerState.Idle;
	private bool _isRunning = false;
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		_anim = GetNode<AnimationPlayer>("Animation");
		_collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
		_position = _collisionShape.Position;
		_anim.AnimationChanged += _animationChange;
		base._Ready();
	}
	// need to set visible finished animation to false
	private void _animationChange(StringName oldName, StringName newName)
	{
		var sprite = GetNode<Sprite2D>(oldName.ToString());
		sprite.Visible = false;

	}
	private void _animate(PlayerState state)
	{
		_state = state;
		var sprite = GetNode<Sprite2D>(state.ToString());
		sprite.FlipH = _velocity.X < 0;
		_anim.Play(state.ToString());
	}
	private void _horizontalMovement(){
		// if keys are pressed it will return 1 for ui_right, -1 for ui_left, and 0 for neither
		var Horizontal_input = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		// horizontal velocity which moves player left or right based on input 
		var isRunning =  Input.GetActionStrength("ui_run_right") +   Input.GetActionStrength("ui_run_left") + 1;
		_velocity.X = Horizontal_input * (Speed*isRunning);
		
	}
	private void _playerAnimation()
	{		
		// on left (add is_action_just_released so you continue running after jumping)
		if (Input.IsActionPressed("ui_run_left"))
		{
			//GD.Print("ui_left.");
			_animate(PlayerState.Run);
		}
		if (Input.IsActionPressed("ui_left"))
		{
			//GD.Print("ui_left.");
			_animate(PlayerState.Walk);
			_position.X -= WalkDistance;
		}

		// on right (add is_action_just_released so you continue running after jumping)
		if (Input.IsActionPressed("ui_right"))
		{
			//GD.Print("ui_right.");
			_animate(PlayerState.Walk);
				_position.X += WalkDistance;
		}

		// on idle if nothing is being pressed
		if (!Input.IsAnythingPressed())
		{	
			_animate(PlayerState.Idle);
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		_velocity = Velocity;
		_position = Position;
		// vertical movement velocity (down)
		// Add the gravity.
		if (!IsOnFloor())
		{
			_velocity.Y += gravity * (float)delta;
			_animate(PlayerState.Jump);
		}
		else
		{
			_horizontalMovement();
			_playerAnimation();
		}
		Velocity = _velocity;
		Position =_position;
		MoveAndSlide();
		base._PhysicsProcess(delta);
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey)
		{
			// on jump
			if (eventKey.IsActionPressed("ui_up") && IsOnFloor())
			{
				//GD.Print("Jump Key pressed.");
				_velocity.Y = -65f;
				_animate(PlayerState.Jump);			
			} 
		
		}
		base._Input(@event);
	}
}
