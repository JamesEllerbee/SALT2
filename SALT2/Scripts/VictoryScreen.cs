using Godot;
using System;

public class VictoryScreen : CanvasLayer
{
	
	public override void _Ready()
	{
		GetNode<AudioStreamPlayer>("VictoryMusic").Play();
		GetNode<AnimatedSprite>("AnimatedSprite").Visible = false;
	}

	private async void _on_AnotherMission_pressed()
	{
		GetNode<AnimatedSprite>("AnimatedSprite").Play("Shoot", false);
		GetNode<AudioStreamPlayer>("ShootSoundFX").Play();
		await ToSignal(GetTree().CreateTimer(0.5f, true), "timeout");
		GetNode<AudioStreamPlayer>("VictoryMusic").Stop();
		GetTree().ChangeScene("res://Scenes/Level.tscn");
	}
	
	private void _on_AnotherMission_mouse_entered()
	{
		GetNode<AnimatedSprite>("AnimatedSprite").Visible = true;
		GetNode<AnimatedSprite>("AnimatedSprite").Position = new Vector2(343f, 285f);
		GetNode<AnimatedSprite>("AnimatedSprite").Play("Walk", false);
		GetNode<AudioStreamPlayer>("SelectSoundFX").Play();
	}
	
	private void _on_AnotherMission_mouse_exited()
	{
		GetNode<AnimatedSprite>("AnimatedSprite").Visible = false;
	}
	
	private void _on_GoHome_pressed()
	{
		GetNode<AudioStreamPlayer>("VictoryMusic").Stop();
		GetTree().ChangeScene("res://Scenes/Menu.tscn");
	}
	
	private void _on_GoHome_mouse_entered()
	{
		GetNode<AnimatedSprite>("AnimatedSprite").Visible = true;
		GetNode<AnimatedSprite>("AnimatedSprite").Play("Walk", false);
		GetNode<AnimatedSprite>("AnimatedSprite").Position = new Vector2(343f, 430f);
		GetNode<AudioStreamPlayer>("SelectSoundFX").Play();
	}
	
	private void _on_GoHome_mouse_exited()
	{
		GetNode<AnimatedSprite>("AnimatedSprite").Visible = false;
	}

}
