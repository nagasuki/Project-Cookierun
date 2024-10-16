using PugDev;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelectionScene : SingletonMonoBehaviour<UILevelSelectionScene>
{
    public Button BackToMainMenuButton;

    public List<Button> StageButtons = new List<Button>();
    [SerializeField] private List<TextMeshProUGUI> highScoreTexts = new List<TextMeshProUGUI>();

    public void UnlockStage(int ableStageId)
    {
        var stageIndex = ableStageId - 1;

        for (int i = 0; i < StageButtons.Count; i++)
        {
            if (i < stageIndex)
            {
                StageButtons[i].interactable = true;
            }
            else
            {
                StageButtons[i].interactable = false;
            }
        }
    }

    public void SetHighScoreText(int ableStageId)
    {
        var stageIndex = ableStageId - 1;

        for (int i = 0; i < highScoreTexts.Count; i++)
        {
            if (i < stageIndex)
            {
                if (GameManager.Instance.GetHighScoreOnStage(ableStageId) > 0)
                {
                    highScoreTexts[i].text = $"High Score\n" +
                    $"{GameManager.Instance.GetHighScoreOnStage(ableStageId).ToString("N0")}";
                }
                else
                {
                    highScoreTexts[i].text = $"High Score\n" +
                    $"<color=red>{0}</color>";
                }
            }
            else
            {
                highScoreTexts[i].text = $"High Score\n" +
                    $"<color=red>{0}</color>";
            }
        }
    }
}
