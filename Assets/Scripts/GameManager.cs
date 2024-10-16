using Cysharp.Threading.Tasks;
using PugDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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

    private void Start()
    {
        GameStateManager.Instance.SetState(new MainMenuState());
    }

    public void GameStart()
    {
        IsGameOver = false;
        IsGameComplete = false;
    }

    public void SetSelectionStageId(int stageId)
    {
        selectionStageId = stageId;
        currentStageId = stageId;
    }

    public async UniTask GameOver(int score)
    {
        SoundManager.Instance.PlayeSFX("Die");

        IsGameOver = true;

        gameOverPopup.SetButtonAction(() =>
        {
            gameOverPopup.HidePopup();

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

        gameOverPopup.ShowPopup();
    }

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
