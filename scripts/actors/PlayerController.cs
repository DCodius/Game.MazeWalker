using Godot;
using System;

public partial class PlayerController : BaseActorController
{
	[Export]
	public  float Speed = 300.0f;

	[Export]
	public  float JumpVelocity = -400.0f;
	[Export]
	public float Acceleration = 0.25f;
	[Export]
	public float Friction = 0.1f;
	private ViewportTexture viewPortTexture;



	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private void _checkInput(double delta,ref Vector2 velocity)
	{
		// Get the input direction and handle the movement/deceleration.
		var direction = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		// Falling 
		if (!IsOnFloor())
			velocity.Y += gravity * (float) delta;

		// handle jump (if grounded)
		if (Input.IsKeyPressed(Key.Space) && IsOnFloor())
			velocity.Y = JumpVelocity;

		// handle horizontal movement (keyboard arrow keys)
		velocity.X = 0;
		if (direction != 0)
		{
			velocity.X =  Mathf.Lerp(velocity.X, direction * Speed, Acceleration);
		}
		else
		{
			velocity.X = Mathf.Lerp(velocity.X, 0, Friction);
		}

	}

	public override void _Ready()
	{
		viewPortTexture = this.GetViewport().GetTexture();
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		_checkInput(delta,ref velocity);
		EmitPlayerVelocity(velocity);
		// As good practice, you should replace UI actions with custom gameplay actions.
		Velocity = velocity;
		MoveAndSlide();
	}
}