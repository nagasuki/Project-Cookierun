using Cysharp.Threading.Tasks;
using PugDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Properties")]
    [SerializeField] private int currentStageId = 0;
    [SerializeField] private List<MapProperties> mapPropertiesList = new List<MapProperties>();

    public bool IsGameOver { get; private set; }

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
    }

    public async UniTask GameOver()
    {
        IsGameOver = true;

        await UniTask.Delay(2500);


    }

    public MapProperties GetMapProperties(int stageId)
    {
        return mapPropertiesList[stageId - 1];
    }

    public int GetLastStageId()
    {
        var lastStageId = PlayerPrefs.GetInt(PlayerPrefsConstants.LAST_STAGE, 1);
        currentStageId = lastStageId;
        return currentStageId;
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
