using Godot;
using System;

public partial class Player : ActorController
{
	[Export]
	public  float Speed = 300.0f;

	[Export]
	public  float JumpVelocity = -400.0f;




	protected PlayerState _currentState = PlayerState.Idle;
	protected AnimationPlayer _anim;



	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private void _checkInput(double delta,ref Vector2 velocity)
	{
		// Get the input direction and handle the movement/deceleration.
		var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		// Add the gravity
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_up") && IsOnFloor())
			velocity.Y = JumpVelocity;
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
	}

	public override void _Ready()
	{
		_anim = GetNode<AnimationPlayer>("Animation");
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		_checkInput(delta,ref velocity);
		// As good practice, you should replace UI actions with custom gameplay actions.
		Velocity = velocity;
		EmitPlayerVelocity(velocity);
		MoveAndSlide();
	}
}