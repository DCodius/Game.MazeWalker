using Godot;
using System;
namespace MazeWalker.Character;
/// <summary>
/// Animated player based on PlayerAnimator selection, so its much easier for animation changes
/// </summary>
public partial class PlayerAnimation : Node2D
{
	/// <summary>
	/// Set to Designated AnimationPlayer
	/// </summary>
	[Export] 
	public AnimatedSprite2D PlayerAnimator;
	[Export]
	public float DefaultTransisitonTime = 0.1f;
	BaseChacaterController controller;
	protected void AnimateActor<T>(T animationState, float transisitonTime)
	{
		PlayerAnimator.Play(animationState.ToString());
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
			if (velocity.X > 0)
			{
				if (PlayerAnimator.FlipH) PlayerAnimator.FlipH = false;
				nextState = PlayerState.Walk;
			}
			if (velocity.X < 0)
			{
				PlayerAnimator.FlipH = true;
				nextState = PlayerState.Walk;
			}

		}
		AnimateActor(nextState, DefaultTransisitonTime);

	}

	public override void _Ready()
	{
		controller = this.FindNodeInParent<BaseChacaterController>();
		if (controller is null)
		{
			GD.PrintErr("No parent containing ActorController");
		}
		else
		{
			controller.ActorVelocityChanged += Controller_ActorVelocityChanged;
		}
		PlayerAnimator = this.FindNodeInChildren<AnimatedSprite2D>();
		if (PlayerAnimator is null)
		{
			GD.PrintErr("No child containing AnimationPlayer");
		}
		else
		{
			AnimateActor(PlayerState.Idle, DefaultTransisitonTime);
		}
		base._Ready();
	}




	private void Controller_ActorVelocityChanged(object sender, ActorVelocityEvent e)
	{
		SetMovementForAnimation(e.Velocity,e.IsOnFloor);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

}
