using System;
using Godot;

/// <summary>
/// The script that controls the main menu.
/// </summary>
public class Menu : Control
{
	/// <inheritdoc/>
	public override void _Ready()
	{
		GetNode<TextureButton>("VBoxContainer/StartButton").GrabFocus();
	}

	// why is this code commented out?
	/*
	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("move_down") && )
		{
			GetNode<Button>("VBoxContainer/OptionsButton").FocusMode;
			GetFocusMode();

		}
	}
	 */

	/// <inheritdoc/>
	public override void _Process(float delta)
	{
		// Makes music loop
		if (!GetNode<AudioStreamPlayer2D>("MainMenuMusic").Playing)
		{
			GetNode<AudioStreamPlayer2D>("MainMenuMusic").Play();
		}
	}

	private void _on_StartButton_pressed()
	{
		GetNode<AudioStreamPlayer>("StartSoundFX").Play();

		// todo: update this to the level scene.
		GetTree().ChangeScene("res://Scenes/dev_testnew.tscn");
	}

	private void _on_QuitButton_pressed()
	{
		GetTree().Quit();
	}

	private void _on_OptionsButton_pressed()
	{
		GetNode<VBoxContainer>("VBoxContainer").Visible = false;
		GetNode<HSlider>("VolumeSlider").Visible = true;
		GetNode<CheckButton>("FullScreenToggle").Visible = true;
		GetNode<TextureButton>("MainMenuButton").Visible = true;
	}

	private void _on_VolumeSlider_value_changed(float value)
	{
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), value);
	}

	private void _on_FullScreenToggle_pressed()
	{
		OS.WindowFullscreen = !OS.WindowFullscreen; // toggles full screeen off/on
	}

	private void _on_MainMenuButton_pressed()
	{
		GetNode<VBoxContainer>("VBoxContainer").Visible = true;
		GetNode<HSlider>("VolumeSlider").Visible = false;
		GetNode<CheckButton>("FullScreenToggle").Visible = false;
		GetNode<TextureButton>("MainMenuButton").Visible = false;
	}

	private void _on_StartButton_mouse_entered()
	{
		GetNode<TextureButton>("VBoxContainer/StartButton").GrabFocus();
	}

	private void _on_OptionsButton_mouse_entered()
	{
		GetNode<TextureButton>("VBoxContainer/OptionsButton").GrabFocus();
	}

	private void _on_QuitButton_mouse_entered()
	{
		GetNode<TextureButton>("VBoxContainer/QuitButton").GrabFocus();
	}

	private void _on_StartButton_focus_entered()
	{
		GetNode<AudioStreamPlayer>("SelectSoundFX").Play();
	}

	private void _on_OptionsButton_focus_entered()
	{
		GetNode<AudioStreamPlayer>("SelectSoundFX").Play();
	}

	private void _on_QuitButton_focus_entered()
	{
		GetNode<AudioStreamPlayer>("SelectSoundFX").Play();
	}
}
