using System;
using Godot;

/// <summary>
/// Script to control the puase menu.
/// </summary>
public class Pause : CanvasLayer
{
	/// <inheritdoc/>
	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("ui_cancel") && !GetTree().CurrentScene.Name.Equals("Menu"))
		{
			GetNode<TextureRect>("BackGround").Visible = !GetNode<TextureRect>("BackGround").Visible;
			GetNode<VBoxContainer>("VBoxContainer").Visible = !GetNode<VBoxContainer>("VBoxContainer").Visible;
			
			GetNode<AnimatedSprite>("SlugAnimation").Play("Idle", false);
			GetNode<AnimatedSprite>("SlugAnimation").Position = new Vector2(635f, 225f);
			GetNode<AnimatedSprite>("SlugAnimation").Visible = !GetNode<AnimatedSprite>("SlugAnimation").Visible;
			
			
			
			//GetNode<AudioStreamPlayer>("PauseMusic").StreamPaused = !GetNode<AudioStreamPlayer>("PauseMusic").StreamPaused;
			
			if(GetNode<VBoxContainer>("VBoxContainer").Visible)
			{
				GetNode<AudioStreamPlayer>("PauseMusic").Play();
			} 
			
			 if(!GetNode<VBoxContainer>("VBoxContainer").Visible)
			{
				GetNode<AudioStreamPlayer>("PauseMusic").Stop();
			} 

			// checks if options are open, will automatically close out of pause menu if it is
			if (GetNode<HSlider>("VolumeSlider").Visible)
			{
				GetNode<AudioStreamPlayer>("PauseMusic").Stop();
				GetNode<VBoxContainer>("VBoxContainer").Visible = false;
				GetNode<AnimatedSprite>("SlugAnimation").Visible = false;
			}

			GetNode<HSlider>("VolumeSlider").Visible = false;
			GetNode<CheckButton>("FullScreenToggle").Visible = false;
			GetNode<TextureButton>("BackButton").Visible = false;
			GetNode<AudioStreamPlayer>("PauseMusic").Stop();
			GetTree().Paused = !GetTree().Paused; // toggles paused status
		}
	}

	public void _on_ContinueButton_pressed()
	{
		GetTree().Paused = false;
		GetNode<TextureRect>("BackGround").Visible = false;
		GetNode<VBoxContainer>("VBoxContainer").Visible = false;
		GetNode<AnimatedSprite>("SlugAnimation").Visible = false;
		GetNode<AudioStreamPlayer>("PauseMusic").Stop();
		GetNode<AudioStreamPlayer>("ContinueSoundFX").Play();
	}

	private void _on_QuitButton_pressed()
	{
		GetNode<TextureRect>("BackGround").Visible = false;
		GetNode<VBoxContainer>("VBoxContainer").Visible = false;
		GetNode<AnimatedSprite>("SlugAnimation").Visible = false;
		GetNode<AudioStreamPlayer>("PauseMusic").Stop();
		GetTree().Paused = false;
		GetTree().ChangeScene("res://Scenes/Menu.tscn");
	}

	private void _on_OptionsButton_pressed()
	{
		GetNode<VBoxContainer>("VBoxContainer").Visible = false;
		GetNode<HSlider>("VolumeSlider").Visible = true;
		GetNode<CheckButton>("FullScreenToggle").Visible = true;
		GetNode<TextureButton>("BackButton").Visible = true;
		GetNode<AnimatedSprite>("SlugAnimation").Position = new Vector2(635f, 525f);
	}

	private void _on_VolumeSlider_value_changed(float value)
	{
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), value);
	}

	private void _on_FullScreenToggle_pressed()
	{
		OS.WindowFullscreen = !OS.WindowFullscreen; // toggles full screeen off/on
	}

	private void _on_BackButton_pressed()
	{
		GetNode<VBoxContainer>("VBoxContainer").Visible = true;
		GetNode<HSlider>("VolumeSlider").Visible = false;
		GetNode<CheckButton>("FullScreenToggle").Visible = false;
		GetNode<TextureButton>("BackButton").Visible = false;
		GetNode<AnimatedSprite>("SlugAnimation").Position = new Vector2(635f, 313f);
	}
	
	private void _on_ContinueButton_mouse_entered()
	{
		GetNode<TextureButton>("VBoxContainer/ContinueButton").GrabFocus();
		GetNode<AnimatedSprite>("SlugAnimation").Position = new Vector2(635f, 225f);
		GetNode<AudioStreamPlayer>("SelectSoundFX").Play();
		
	}
	
	private void _on_OptionsButton_mouse_entered()
	{
		GetNode<TextureButton>("VBoxContainer/OptionsButton").GrabFocus();
		GetNode<AnimatedSprite>("SlugAnimation").Position = new Vector2(635f, 313f);
		GetNode<AudioStreamPlayer>("SelectSoundFX").Play();
	}
	
	private void _on_QuitButton_mouse_entered()
	{
		GetNode<TextureButton>("VBoxContainer/QuitButton").GrabFocus();
		GetNode<AnimatedSprite>("SlugAnimation").Position = new Vector2(635f, 397f);
		GetNode<AudioStreamPlayer>("SelectSoundFX").Play();
	}
}
