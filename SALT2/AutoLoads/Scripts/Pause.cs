using Godot;
using System;

public class Pause : CanvasLayer
{

	public override void _Input(InputEvent inputEvent)
{
	if (inputEvent.IsActionPressed("ui_cancel"))
	{
		GetNode<TextureRect>("BackGround").Visible = !GetNode<TextureRect>("BackGround").Visible;
		GetTree().Paused = !GetTree().Paused; //toggles paused status
	}
}

public void _on_ContinueButton_pressed()
{
	GetTree().Paused = false;
	GetNode<TextureRect>("BackGround").Visible = false;
}

private void _on_QuitButton_pressed()
{
	//TODO: This will change scene to main menu once main menu is in root
	//GetTree().ChangeScene()
}

private void _on_OptionsButton_pressed()
{
	//TODO: Add Functionality for options
}


}
