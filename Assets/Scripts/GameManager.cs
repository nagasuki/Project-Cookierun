using Cysharp.Threading.Tasks;
using PugDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

/// <summary>
/// Manages the game state and provides methods for gameplay-related operations.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Properties")]
    [SerializeField] private int currentStageId = 0;
    [SerializeField] private List<MapProperties> mapPropertiesList = new List<MapProperties>();

    [Header("Popups")]
    [SerializeField] private GenericPopup gameCompletePopup;
    [SerializeField] private GenericPopup gameOverPopup;

    private int selectionStageId = 0;
    public int SelectionStageId => selectionStageId;

    public bool IsGameOver { get; private set; }
    public bool IsGameComplete { get; private set; }

    /// <summary>
    /// Initializes the GameManager singleton instance.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Starts the game by setting the game state to MainMenuState.
    /// </summary>
    private void Start()
    {
        GameStateManager.Instance.SetState(new MainMenuState());
    }

    /// <summary>
    /// Resets the game state to default values.
    /// </summary>
    public void GameStart()
    {
        IsGameOver = false;
        IsGameComplete = false;
    }

    /// <summary>
    /// Sets the selected stage ID.
    /// </summary>
    /// <param name="stageId">The ID of the selected stage.</param>
    public void SetSelectionStageId(int stageId)
    {
        selectionStageId = stageId;
        currentStageId = stageId;
    }

    /// <summary>
    /// Displays the game over popup and handles the game over logic.
    /// </summary>
    /// <param name="score">The player's score.</param>
    /// <returns>A UniTask representing the async operation.</returns>
    public async UniTask GameOver(int score)
    {
        SoundManager.Instance.PlayeSFX("Die");

        IsGameOver = true;

        gameOverPopup.SetButtonAction(() =>
        {
            gameOverPopup.HidePopup();

            if (score > GetHighScoreOnStage(currentStageId))
            {
                PlayerPrefs.SetInt(PlayerPrefsConstants.HIGHSCORE_STAGE + currentStageId, score);
            }

            LoadSceneManager.Instance.LoadSceneAsync("LevelSelectionScene", onChangeGameState: () =>
            {
                GameStateManager.Instance.SetState(new LevelSelectionState());
            }).Forget();
        });

        gameOverPopup.ShowPopup();
    }

    /// <summary>
    /// Displays the game complete popup and handles the game complete logic.
    /// </summary>
    /// <param name="score">The player's score.</param>
    /// <returns>A UniTask representing the async operation.</returns>
    public async UniTask GameComplete(int score)
    {
        IsGameComplete = true;

        gameCompletePopup.SetText(score.ToString("N0"));
        gameCompletePopup.SetButtonAction(() =>
        {
            gameCompletePopup.HidePopup();

            PlayerPrefs.SetInt(PlayerPrefsConstants.LAST_STAGE, Mathf.Clamp(currentStageId + 1, 1, 2));
            
            if (score > GetHighScoreOnStage(currentStageId))
            {
                PlayerPrefs.SetInt(PlayerPrefsConstants.HIGHSCORE_STAGE + currentStageId, score);
            }

            LoadSceneManager.Instance.LoadSceneAsync("LevelSelectionScene", onChangeGameState: () =>
            {
                GameStateManager.Instance.SetState(new LevelSelectionState());
            }).Forget();
        });

        gameCompletePopup.ShowPopup();
    }

    public MapProperties GetMapProperties(int stageId)
    {
        return mapPropertiesList[stageId - 1];
    }

    public int GetLastStageId()
    {
        var lastStageId = PlayerPrefs.GetInt(PlayerPrefsConstants.LAST_STAGE, 1);
        return lastStageId;
    }

    public void StageClear()
    {
        PlayerPrefs.SetInt(PlayerPrefsConstants.LAST_STAGE, currentStageId + 1);
    }

    public int GetHighScoreOnStage(int stageId)
    {
        var highScore = PlayerPrefs.GetInt(PlayerPrefsConstants.HIGHSCORE_STAGE + stageId, 0);
        return highScore;
    }

    public void SaveHighScoreOnStage(int stageId, int highScore)
    {
        PlayerPrefs.SetInt(PlayerPrefsConstants.HIGHSCORE_STAGE + stageId, highScore);
    }
}
