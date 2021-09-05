using Godot;
using System;

public class Pause : CanvasLayer
{

	public override void _Input(InputEvent inputEvent)
{
	if (inputEvent.IsActionPressed("ui_cancel") && !GetTree().CurrentScene.Name.Equals("Menu"))
	{
		
		GetNode<TextureRect>("BackGround").Visible = !GetNode<TextureRect>("BackGround").Visible;
		GetNode<VBoxContainer>("VBoxContainer").Visible = !GetNode<VBoxContainer>("VBoxContainer").Visible;
		
		//checks if options are open, will automatically close out of pause menu if it is
		if(GetNode<HSlider>("VolumeSlider").Visible == true){
			GetNode<VBoxContainer>("VBoxContainer").Visible = false;
		}
		
		GetNode<HSlider>("VolumeSlider").Visible = false;
		GetNode<CheckButton>("FullScreenToggle").Visible = false;
		GetNode<Button>("BackButton").Visible = false;
		GetTree().Paused = !GetTree().Paused; //toggles paused status
	}
}

public void _on_ContinueButton_pressed()
{
	GetTree().Paused = false;
	GetNode<TextureRect>("BackGround").Visible = false;
	GetNode<VBoxContainer>("VBoxContainer").Visible = false;
}

private void _on_QuitButton_pressed()
{
	GetNode<TextureRect>("BackGround").Visible = false;
	GetNode<VBoxContainer>("VBoxContainer").Visible = false;
	GetTree().Paused = false;
	GetTree().ChangeScene("res://Scenes/Menu.tscn");
}

private void _on_OptionsButton_pressed()
{
	GetNode<VBoxContainer>("VBoxContainer").Visible = false;
	GetNode<HSlider>("VolumeSlider").Visible = true;
	GetNode<CheckButton>("FullScreenToggle").Visible = true;
	GetNode<Button>("BackButton").Visible = true;
}

private void _on_VolumeSlider_value_changed(float value)
{
	AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), value);
}

private void _on_FullScreenToggle_pressed()
{
	OS.WindowFullscreen = !OS.WindowFullscreen; //toggles full screeen off/on
}

private void _on_BackButton_pressed()
{
	GetNode<VBoxContainer>("VBoxContainer").Visible = true;
	GetNode<HSlider>("VolumeSlider").Visible = false;
	GetNode<CheckButton>("FullScreenToggle").Visible = false;
	GetNode<Button>("BackButton").Visible = false;
}

}
