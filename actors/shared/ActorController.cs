using Godot;
using System;

public partial class ActorController : CharacterBody2D
{
	public event EventHandler<ActorVelocityEvent> PlayerVelocityChanged;
	// Called when the node enters the scene tree for the first time.
	protected void EmitPlayerVelocity(Vector2 newVelocity)
	{
		PlayerVelocityChanged?.Invoke(this,new ActorVelocityEvent(newVelocity));
	}
}
