using Godot;
using System;

public class ActorVelocityEvent:EventArgs
{
	public Vector2 Velocity { get; }
	public bool IsOnFloor { get; }
	public ActorVelocityEvent(Vector2 velocity,bool isOnFloor)
	{
		Velocity = velocity;
		IsOnFloor = isOnFloor;
	}
}
