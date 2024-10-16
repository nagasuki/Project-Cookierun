using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// View model for the score.
/// </summary>
public class ScoreViewModel : MonoBehaviour
{
    /// <summary>
    /// The score model.
    /// </summary>
    public ScoreModel scoreModel;

    /// <summary>
    /// The current score.
    /// </summary>
    public int CurrentScore => scoreModel.CurrentScore;

    /// <summary>
    /// Called when the component is enabled.
    /// </summary>
    private void OnEnable()
    {
        // Create a new score model and subscribe to its OnScoreChanged event.
        scoreModel = new ScoreModel();
        scoreModel.OnScoreChanged += OnScoreChanged;
    }

    /// <summary>
    /// Called when the component is disabled.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe from the OnScoreChanged event of the score model.
        scoreModel.OnScoreChanged -= OnScoreChanged;
    }

    /// <summary>
    /// Called when the score is changed.
    /// </summary>
    /// <param name="newScore">The new score.</param>
    private void OnScoreChanged(int newScore)
    {
        Debug.Log("Score updated to: " + newScore);
    }

    /// <summary>
    /// Adds the specified number of points to the score.
    /// </summary>
    /// <param name="points">The number of points to add.</param>
    public void AddScore(int points)
    {
        scoreModel.AddScore(points);
    }

    /// <summary>
    /// Resets the score to zero.
    /// </summary>
    public void ResetScore()
    {
        scoreModel.ResetScore();
    }
}
