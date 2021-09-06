using Godot;
using System;

public class MissionStartController : Control
{
    private AnimationPlayer alertAnim;
    private AnimationPlayer alertBlinkAnim;
    private AnimationPlayer missionAnim;
    private AnimationPlayer startAnim;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        alertAnim = (AnimationPlayer)GetNode("Alert/AlertAnim");
        alertBlinkAnim = (AnimationPlayer)GetNode("Alert/AlertBlinkAnim");
        missionAnim = (AnimationPlayer)GetNode("Mission/MissionAnim");
        startAnim = (AnimationPlayer)GetNode("Start/StartAnim");

        alertAnim.Play("alertAppear");
    }

    public void _on_AlertAnim_animation_finished(String animation)
    {
        if (animation == "alertAppear")
        {
            alertBlinkAnim.Play("alertBlink");
            missionAnim.Play("missionAppear");
        }
    }

    public void _on_MissionAnim_animation_finished(String animation)
    {
        if (animation == "missionAppear")
        {
            startAnim.Play("startBlink");
            alertBlinkAnim.Stop();
        }
    }

    public void _on_StartAnim_animation_finished(String animation)
    {
        if (animation == "startBlink")
        {
            QueueFree();
        }
    }
}
