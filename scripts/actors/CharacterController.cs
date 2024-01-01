using Godot;
using System;

public partial class BaseActorController : CharacterBody2D
{
	public event EventHandler<ActorVelocityEvent> ActorVelocityChanged;
	private Vector2 lastVelocity;
	// Called when the node enters the scene tree for the first time.
	protected void EmitPlayerVelocity(Vector2 newVelocity)
	{
		//if (newVelocity.Equals(lastVelocity)) return;
		//lastVelocity = newVelocity;
		ActorVelocityChanged?.Invoke(this,new ActorVelocityEvent(newVelocity,IsOnFloor()));
	}
}
