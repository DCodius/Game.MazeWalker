using Godot;
using System;

namespace MazeWalker.Character;
/// <summary>
/// Strong Typed Stated for easy code completion and avoid Typo !
/// </summary>
public enum PlayerState
{
	Idle,
	Walk,
	Run,
	Jump,
	Hurt,
}