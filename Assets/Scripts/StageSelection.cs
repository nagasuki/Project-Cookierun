using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that handles stage selection in the game.
/// </summary>
public class StageSelection : MonoBehaviour
{
    /// <summary>
    /// Selects a stage with the given ID and updates the game manager's selection stage ID.
    /// </summary>
    /// <param name="stageId">The ID of the stage to select.</param>
    public void SelectStage(int stageId)
    {
        // Call the SetSelectionStageId method of the GameManager singleton instance, passing in the stage ID.
        GameManager.Instance.SetSelectionStageId(stageId);
    }
}
