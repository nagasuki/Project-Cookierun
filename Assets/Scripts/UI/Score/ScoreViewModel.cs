using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreViewModel : MonoBehaviour
{
    public ScoreModel scoreModel;

    public int CurrentScore => scoreModel.CurrentScore;

    void Start()
    {
        scoreModel.OnScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int newScore)
    {
        Debug.Log("Score updated to: " + newScore);
    }

    public void AddScore(int points)
    {
        scoreModel.AddScore(points);
    }

    public void ResetScore()
    {
        scoreModel.ResetScore();
    }
}
