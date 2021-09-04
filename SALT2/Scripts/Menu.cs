using Godot;
using System;

public class Menu : Control
{
	public override void _Ready()
	{
		GetNode<Button>("VBoxContainer/StartButton").GrabFocus();
	}
	
	// public override void _Input(InputEvent inputEvent)
	// {
	// 	if (inputEvent.IsActionPressed("move_down") && )
	// 		{
	// 			GetNode<Button>("VBoxContainer/OptionsButton").FocusMode;
	// 			GetFocusMode();
				
	// 		}
	// }



	private void _on_StartButton_pressed()
	{
	GetTree().ChangeScene("res://Scenes/dev_test1.tscn");
	}
	
	private void _on_QuitButton_pressed()
	{
	GetTree().Quit();
	}
	
	private void _on_StartButton_mouse_entered()
	{
   		GetNode<Button>("VBoxContainer/StartButton").GrabFocus();
	}
	
	private void _on_OptionsButton_mouse_entered()
	{
		GetNode<Button>("VBoxContainer/OptionsButton").GrabFocus();
	}
	
	private void _on_QuitButton_mouse_entered()
	{
		GetNode<Button>("VBoxContainer/QuitButton").GrabFocus();
	}

}
