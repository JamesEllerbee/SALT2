using Godot;
using System;

public class WincumberController : StaticBody
{
    [Export]
    private float descendSpeed = 5;

    private BossController boss;
    private CollisionShape outer;
    private CollisionShape inner;
    private Spatial graphics;
    private AnimationPlayer anim;

    private bool bossDead = false;

    /// <inheritdoc/>
    public override void _Ready()
    {
        boss = GetNode<BossController>("/root/Main/Boss");
        outer = (CollisionShape)GetNode("CollisionShape");
        inner = (CollisionShape)GetNode("Area/CollisionShape");
        graphics = (Spatial)GetNode("ACUMBblock");
        anim = (AnimationPlayer)GetNode("AnimationPlayer");
    }

    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        base._Process(delta);

        if (boss.CurrentHealth <= 0 && !bossDead)
        {
            bossDead = true;
            Enter();
        }
    }

    public void _on_Area_body_entered(PhysicsBody body)
    {
        if (body.IsInGroup("Player"))
        {
            GetTree().ChangeScene("res://Prefabs/VictoryScreen.tscn");
        }
    }

    private void Enter()
    {
        outer.Disabled = false;
        inner.Disabled = false;
        graphics.Visible = true;
        anim.Play("entrance");
    }
}
