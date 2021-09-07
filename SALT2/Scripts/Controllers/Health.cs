using System;
using Godot;

/// <summary>
/// Script to control health UI.
/// </summary>
public class Health : Control
{
    private PlayerController player;

    private Sprite ticket1;
    private Sprite ticket2;
    private Sprite ticket3;

    /// <inheritdoc/>
    public override void _Ready()
    {
        player = GetNode<PlayerController>("/root/Main/Player");

        ticket1 = GetNode<Sprite>("Ticket1");
        ticket2 = GetNode<Sprite>("Ticket2");
        ticket3 = GetNode<Sprite>("Ticket3");
    }

    /// <inheritdoc/>
    public override void _Process(float delta)
    {
        base._Process(delta);

        // todo: add missing ticket sprites here.
        if (player.CurrentHealth == 3)
        {
            ticket1.Visible = true;
            ticket2.Visible = true;
            ticket3.Visible = true;
        }
        else if (player.CurrentHealth == 2)
        {
            ticket1.Visible = true;
            ticket2.Visible = true;
            ticket3.Visible = false;
        }
        else if (player.CurrentHealth == 1)
        {
            ticket1.Visible = true;
            ticket2.Visible = false;
            ticket3.Visible = false;
        }
        else if (player.CurrentHealth <= 0)
        {
            ticket1.Visible = false;
            ticket2.Visible = false;
            ticket3.Visible = false;
        }
    }
}
