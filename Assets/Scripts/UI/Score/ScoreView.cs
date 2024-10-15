using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public ScoreViewModel viewModel;

    void Start()
    {
        UpdateScore(viewModel.CurrentScore);

        viewModel.scoreModel.OnScoreChanged += UpdateScore;
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString("N0");
    }
}
