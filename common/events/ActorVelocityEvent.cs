using Godot;
using System;

public class ActorVelocityEvent:EventArgs
{
	public Vector2 Velocity { get; }
	public ActorVelocityEvent(Vector2 velocity)
	{
		Velocity = velocity;
	}
}
