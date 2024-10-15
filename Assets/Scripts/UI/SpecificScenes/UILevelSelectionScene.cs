using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelectionScene : MonoBehaviour
{
    [SerializeField] private List<Button> stageButtons = new List<Button>();

    public void UnlockStage(int ableStageId)
    {
        var stageIndex = ableStageId - 1;

        for (int i = 0; i < stageButtons.Count; i++)
        {
            if (i < stageIndex)
            {
                stageButtons[i].interactable = true;
            }
            else
            {
                stageButtons[i].interactable = false;
            }
        }
    }
}
