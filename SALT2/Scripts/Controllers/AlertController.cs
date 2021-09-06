using Godot;
using System;

public class AlertController : Control
{
    private AnimationPlayer alertAnim;
    private AnimationPlayer alertBlinkAnim;
    private AnimationPlayer emergAnim;
    private AnimationPlayer destroyAnim;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        alertAnim = (AnimationPlayer)GetNode("Alert/AlertAnim");
        alertBlinkAnim = (AnimationPlayer)GetNode("Alert/AlertBlinkAnim");
        emergAnim = (AnimationPlayer)GetNode("Emergency/EmergencyAnim");
        destroyAnim = (AnimationPlayer)GetNode("Destroy/DestroyAnim");

        alertAnim.Play("alertAppear");
    }

    public void _on_AlertAnim_animation_finished(String animation)
    {
        if (animation == "alertAppear")
        {
            alertBlinkAnim.Play("alertBlink");
            emergAnim.Play("emergencyAppear");
        }
    }

    public void _on_EmergencyAnim_animation_finished(String animation)
    {
        if (animation == "emergencyAppear")
        {
            destroyAnim.Play("destroyBlink");
            alertBlinkAnim.Stop();
        }
    }

    public void _on_DestroyAnim_animation_finished(String animation)
    {
        if (animation == "destroyBlink")
        {
            QueueFree();
        }
    }
}
