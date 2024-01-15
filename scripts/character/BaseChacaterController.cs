using Godot;
using System;

namespace MazeWalker.Character;
public partial class BaseChacaterController : CharacterBody2D
{
	public event EventHandler<ActorVelocityEvent> ActorVelocityChanged;
	private Vector2 lastVelocity;
	// Called when the node enters the scene tree for the first time.
	protected void EmitPlayerVelocity(Vector2 newVelocity)
	{
		ActorVelocityChanged?.Invoke(this,new ActorVelocityEvent(newVelocity,IsOnFloor()));
	}
}
