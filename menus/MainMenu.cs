using Godot;
using System;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	//public Button StartButton { get; private set; } = default!;
	public override void _Ready()
	{
		  //  => StartButton = GetNode<Button>("%StartButton");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	 private void OnButtonPressed() 
	 {
    //ButtonPresses++;
    //TestButton.Text = $"Button Pressed {ButtonPresses}";
    GetTree().ChangeSceneToFile("res://scenes/level/Level_1.tscn");
  }
}
