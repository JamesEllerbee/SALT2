using System;
using Godot;

/// <summary>
/// Scripts to control score.
/// </summary>
public class ScoreController : Control
{
    private Label scoreLabel;
    private object syncObject = new object();

    /// <inheritdoc/>
    public override void _Ready()
    {
        scoreLabel = GetNode<Label>("ScoreLabel");
        if (scoreLabel == null)
        {
            GD.PrintErr("Could not resolve score label");
        }

        scoreLabel.Text = "0";
    }

    /// <summary>
    /// Addes the <paramref name="value"/> to the current score.
    /// </summary>
    /// <param name="value"> The value to add to the current score. </param>
    public void Add(int value)
    {
        lock (syncObject)
        {
            var currentScore = Int32.Parse(scoreLabel.Text);
            currentScore += value;
            scoreLabel.Text = currentScore.ToString();
        }
    }
}
