using Godot;
using System;

public partial class ActorController : CharacterBody2D
{
	public event EventHandler<ActorVelocityEvent> ActorVelocityChanged;
	// Called when the node enters the scene tree for the first time.
	protected void EmitPlayerVelocity(Vector2 newVelocity)
	{
		ActorVelocityChanged?.Invoke(this,new ActorVelocityEvent(newVelocity,IsOnFloor()));
	}
}
