using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// A MonoBehaviour that represents the view for the score.
/// </summary>
public class ScoreView : MonoBehaviour
{
    /// <summary>
    /// The TextMeshProUGUI component used to display the score.
    /// </summary>
    public TextMeshProUGUI scoreText;

    /// <summary>
    /// The view model for the score.
    /// </summary>
    public ScoreViewModel viewModel;

    /// <summary>
    /// Initializes the view and subscribes to the OnScoreChanged event of the score model.
    /// </summary>
    void Start()
    {
        UpdateScore(viewModel.CurrentScore);

        viewModel.scoreModel.OnScoreChanged += UpdateScore;
    }

    /// <summary>
    /// Updates the score text with the new score.
    /// </summary>
    /// <param name="newScore">The new score to display.</param>
    private void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString("N0");
    }
}
