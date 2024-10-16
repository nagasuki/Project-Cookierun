using Cysharp.Threading.Tasks;
using PugDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionState : IGameState
{
    private UILevelSelectionScene UI;

    public async UniTask EnterAsync()
    {
        Debug.Log("Enter LevelSelectionState");
        UI = UILevelSelectionScene.Instance;

        var stageId = GameManager.Instance.GetLastStageId();
        Debug.Log($"Stage ID : {stageId}");

        UI.UnlockStage(stageId);
        UI.LoadHighScoreText();

        for (int i = 0; i < UI.StageButtons.Count; i++)
        {
            if (i <= stageId - 1)
            {
                UI.StageButtons[i].onClick.AddListener(() =>
                {
                    LoadSceneManager.Instance.LoadSceneAsync("GameplayScene", onChangeGameState: () =>
                    {
                        GameStateManager.Instance.SetState(new GameStartState());
                    }).Forget();
                });
            }
        }

        UI.BackToMainMenuButton.onClick.AddListener(() =>
        {
            UI.BackToMainMenuButton.interactable = false;

            LoadSceneManager.Instance.LoadSceneAsync("MainMenuScene", onChangeGameState: () =>
            {
                GameStateManager.Instance.SetState(new MainMenuState());
            }).Forget();
        });
    }

    public void Exit()
    {
        Debug.Log("Exit LevelSelectionState");
    }

    public void Update()
    {
        
    }
}
