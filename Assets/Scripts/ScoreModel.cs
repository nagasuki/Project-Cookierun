using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the model for the score in a game.
/// </summary>
public class ScoreModel
{
    private int _currentScore;

    /// <summary>
    /// Event that is raised when the score changes.
    /// </summary>
    public delegate void ScoreChanged(int newScore);

    /// <summary>
    /// Event that is raised when the score changes.
    /// </summary>
    public event ScoreChanged OnScoreChanged;

    /// <summary>
    /// Gets the current score.
    /// </summary>
    public int CurrentScore
    {
        get => _currentScore;
        private set
        {
            _currentScore = value;
            OnScoreChanged?.Invoke(_currentScore);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScoreModel"/> class.
    /// </summary>
    public ScoreModel()
    {
        _currentScore = 0;
    }

    /// <summary>
    /// Adds the specified number of points to the score.
    /// </summary>
    /// <param name="points">The number of points to add.</param>
    public void AddScore(int points)
    {
        CurrentScore += points;
    }

    /// <summary>
    /// Resets the score to zero.
    /// </summary>
    public void ResetScore()
    {
        CurrentScore = 0;
    }
}
