
using Godot;
using System;
public static class NodeExtensions
{
	public static Error EmitSignalFromHandler(this GodotObject godotObject, StringName signal, params Variant[] args)
	{
		var signalName = signal.ToString();
		if (signalName.EndsWith("EventHandler"))
		{
			signalName = signalName.Substr(0, signalName.Length - "EventHandler".Length);
		}
		return godotObject.EmitSignal(signalName, args);
	}
	/// <summary>
	/// Find node in parent
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="node"></param>
	/// <returns></returns>
	public static T FindNodeInParent<T>(this Node node) where T : Node
	{
		while (node is not null)
		{
			var foundNode = node as T;
			if (foundNode is not null)
			{
				return foundNode;
			}
			node = node.GetParent();
		}
		return null;
	}
	/// <summary>
	/// Find node in children
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="node"></param>
	/// <returns></returns>
	public static T FindNodeInChildren<T>(this Node node) where T : Node
	{
		foreach (var child in node.GetChildren())
		{
			var foundNode = child as T;
			if (foundNode is not null)
			{
				return foundNode;
			}
			foundNode = FindNodeInChildren<T>(child);
			if (foundNode is not null)
			{
				return foundNode;
			}
		}
		return null;
	}
}