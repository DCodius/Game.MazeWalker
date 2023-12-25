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
	protected void AnimateActor<T>(T animationState, float transisitonTime)
	{
		PlayerAnimator.Play(animationState.ToString(), customBlend: transisitonTime);
	}

	protected void SetMovementForAnimation(Vector2 velocity,bool isOnFLoor)
	{
		var nextState = PlayerState.Idle;
		if (!isOnFLoor)
		{
			nextState = (velocity.Y < 0) ? PlayerState.Jump : PlayerState.Idle;
		}
		else
		{
			if (Mathf.Abs(velocity.X) > 0)
			{
				nextState = PlayerState.Run;
			}
		}
		AnimateActor(nextState, DefaultTransisitonTime);

	}
	public override void _Ready()
	{
		controller = this.FindNodeInParent<ActorController>();
		if (controller is null)
		{
			GD.PrintErr("No parent containing ActorController");
		}
		else
		{
			controller.ActorVelocityChanged += Controller_ActorVelocityChanged;
		}
		PlayerAnimator = this.FindNodeInChildren<AnimationPlayer>();
		if (PlayerAnimator is null)
		{
			GD.PrintErr("No child containing AnimationPlayer");
		}
		else
		{
			AnimateActor(PlayerState.Idle, DefaultTransisitonTime);
		}
	}

	private void Controller_ActorVelocityChanged(object sender, ActorVelocityEvent e)
	{
		SetMovementForAnimation(e.Velocity,e.IsOnFloor);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

}
