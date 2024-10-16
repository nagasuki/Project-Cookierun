using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelection : MonoBehaviour
{
    public void SelectStage(int stageId)
    {
        GameManager.Instance.SetSelectionStageId(stageId);
    }
}
