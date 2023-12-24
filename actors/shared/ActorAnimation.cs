using Godot;
using System;
/// <summary>
/// Animated player based on PlayerAnimator selection, so its much easier for animation changes
/// </summary>
public partial class ActorAnimation : Node2D
{
	/// <summary>
	/// Set to Designated AnimationPlayer
	/// </summary>
	[Export] 
	public AnimationPlayer PlayerAnimator;
	[Export]
	public float DefaultTransisitonTime = 0.1f;
	ActorController controller;
	protected void Animate<T>(T animationState, float transisitonTime)
	{
		PlayerAnimator.Play(animationState.ToString(), customBlend: transisitonTime);
	}
	protected void OnPlayerVelocityChhanged(Vector2 velocity)
	{
		var nextState = PlayerState.Idle;
		if (Mathf.Abs(velocity.X) > 0)
		{
			nextState = PlayerState.Walk;
		}
		Animate(nextState, DefaultTransisitonTime);

	}
	public override void _Ready()
	{
		Animate(PlayerState.Idle, DefaultTransisitonTime);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
