using Godot;
using System;

public class GameOverScreen : CanvasLayer
{
	public override void _Ready()
	{
		// Makes music loop
		if (!GetNode<AudioStreamPlayer>("GameOverMusic").Playing)
		{
			GetNode<AudioStreamPlayer>("GameOverMusic").Play(1.0f);
		}
		
		GetNode<AnimatedSprite>("AnimatedSprite").Play("Walk");
		
	}


	private async void _on_PlayAgainButton_pressed()
	{
	GetNode<AudioStreamPlayer>("GameOverMusic").Stop();
	GetNode<AnimatedSprite>("AnimatedSprite").Play("Shoot");
	GetNode<AudioStreamPlayer>("ShootSoundFX").Play();
	await ToSignal(GetTree().CreateTimer(0.5f, true), "timeout");
	GetTree().ChangeScene("res://Scenes/Level.tscn");
	}
	
	private async void _on_Quit_pressed()
	{
	GetNode<AudioStreamPlayer>("GameOverMusic").Stop();
	GetNode<AnimatedSprite>("AnimatedSprite").Play("Death");
	GetNode<AudioStreamPlayer>("DeathSoundFX").Play();
	await ToSignal(GetTree().CreateTimer(0.5f, true), "timeout");
	GetTree().ChangeScene("res://Scenes/Menu.tscn");
	
	}
	
	private void _on_Quit_mouse_entered()
	{
	GetNode<AnimatedSprite>("AnimatedSprite").Visible = true;
	GetNode<Sprite>("BeggingSlug").Visible = true;
	GetNode<Label>("BeggingText").Visible = true;
	GetNode<AudioStreamPlayer>("SelectSoundFX").Play();
	GetNode<AnimatedSprite>("AnimatedSprite").Position = new Vector2(740f, 295f);
	
	}
	
	private void _on_Quit_mouse_exited()
	{
	GetNode<Sprite>("BeggingSlug").Visible = false;
	GetNode<Label>("BeggingText").Visible = false;
	GetNode<AnimatedSprite>("AnimatedSprite").Visible = false;
	}
	
	private void _on_PlayAgainButton_mouse_entered()
	{
	GetNode<AnimatedSprite>("AnimatedSprite").Visible = true;
	GetNode<Sprite>("OmniSlug").Visible = true;
	GetNode<Label>("OmniSlugText").Visible = true;
	GetNode<AudioStreamPlayer>("SelectSoundFX").Play();
	GetNode<AnimatedSprite>("AnimatedSprite").Position = new Vector2(53f, 295f);
	}
	
	private void _on_PlayAgainButton_mouse_exited()
	{
	GetNode<Sprite>("OmniSlug").Visible = false;
	GetNode<Label>("OmniSlugText").Visible = false;
	GetNode<AnimatedSprite>("AnimatedSprite").Visible = false;
	}

}
