using Godot;
using System;

public class Menu : Control
{
	public override void _Ready()
	{
		GetNode<Button>("VBoxContainer/StartButton").GrabFocus();
	}


	private void _on_StartButton_pressed()
	{
	GetTree().ChangeScene("res://Scenes/dev_test1.tscn");
	}
	
	private void _on_QuitButton_pressed()
	{
	GetTree().Quit();
	}
	
	
	
}
