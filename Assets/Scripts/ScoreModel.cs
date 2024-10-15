using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel
{
    private int _currentScore;

    public delegate void ScoreChanged(int newScore);
    public event ScoreChanged OnScoreChanged;

    public int CurrentScore
    {
        get => _currentScore;
        private set
        {
            _currentScore = value;
            OnScoreChanged?.Invoke(_currentScore);
        }
    }

    public ScoreModel()
    {
        _currentScore = 0;
    }

    public void AddScore(int points)
    {
        CurrentScore += points;
    }

    public void ResetScore()
    {
        CurrentScore = 0;
    }
}
