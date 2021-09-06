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
        GetNode<AnimatedSprite>("AnimatedSprite").Play("Idle", false);
    }


    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        // Makes music loop
        if (!GetNode<AudioStreamPlayer2D>("MainMenuMusic").Playing)
        {
            GetNode<AudioStreamPlayer2D>("MainMenuMusic").Play();
        }
    }

    private async void _on_StartButton_pressed()
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Play("Shoot", false);
        GetNode<AudioStreamPlayer>("StartSoundFX").Play();

        await ToSignal(GetTree().CreateTimer(0.5f, true), "timeout");

        GetTree().ChangeScene("res://Scenes/Level.tscn");
    }

    private void _on_StartButton_mouse_exited()
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Play("Walk", false);
    }


    private async void _on_QuitButton_pressed()
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Play("Death", false);
        await ToSignal(GetTree().CreateTimer(0.5f, true), "timeout");
        GetTree().Quit();
    }

    private async void _on_OptionsButton_pressed()
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Play("Jump", false);
        await ToSignal(GetTree().CreateTimer(0.5f, true), "timeout");
        GetNode<VBoxContainer>("VBoxContainer").Visible = false;
        GetNode<HSlider>("VolumeSlider").Visible = true;
        GetNode<CheckButton>("FullScreenToggle").Visible = true;
        GetNode<TextureButton>("MainMenuButton").Visible = true;
        GetNode<TextureButton>("MainMenuButton").GrabFocus();
    }

    private void _on_VolumeSlider_value_changed(float value)
    {
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), value);
    }

    private void _on_FullScreenToggle_pressed()
    {
        OS.WindowFullscreen = !OS.WindowFullscreen; // toggles full screeen off/on
    }

    private async void _on_MainMenuButton_pressed()
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Play("Jump", false);
        await ToSignal(GetTree().CreateTimer(0.5f, true), "timeout");
        GetNode<AnimatedSprite>("AnimatedSprite").Play("Walk", false);
        GetNode<VBoxContainer>("VBoxContainer").Visible = true;
        GetNode<HSlider>("VolumeSlider").Visible = false;
        GetNode<CheckButton>("FullScreenToggle").Visible = false;
        GetNode<TextureButton>("MainMenuButton").Visible = false;
        GetNode<AnimatedSprite>("AnimatedSprite").Position = new Vector2(230f, 450.083f);
    }

    private void _on_StartButton_mouse_entered()
    {
        GetNode<TextureButton>("VBoxContainer/StartButton").GrabFocus();
        GetNode<AnimatedSprite>("AnimatedSprite").Position = new Vector2(230f, 367.114f);

    }

    private void _on_OptionsButton_mouse_entered()
    {
        GetNode<TextureButton>("VBoxContainer/OptionsButton").GrabFocus();
        GetNode<AnimatedSprite>("AnimatedSprite").Position = new Vector2(230f, 450.083f);

    }

    private void _on_QuitButton_mouse_entered()
    {
        GetNode<TextureButton>("VBoxContainer/QuitButton").GrabFocus();
        GetNode<AnimatedSprite>("AnimatedSprite").Position = new Vector2(230f, 534.173f);
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

    private void _on_MainMenuButton_focus_entered()
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Position = new Vector2(645f, 446f);
        GetNode<AnimatedSprite>("AnimatedSprite").Play("Walk", false);
    }
}
