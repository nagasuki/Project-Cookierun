using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// A generic popup UI component that can be used to display a message and an action button.
/// </summary>
public class GenericPopup : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private CanvasGroup canvasGroup; // The CanvasGroup component that controls the popup's visibility and interaction.
    [SerializeField] private Button button; // The button component that triggers the popup's action.
    [SerializeField] private TextMeshProUGUI text; // The text component that displays the popup's message.

    /// <summary>
    /// Clears any previously set button action when the component is disabled.
    /// </summary>
    private void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    /// <summary>
    /// Sets the text of the popup message.
    /// </summary>
    /// <param name="newText">The new text to display.</param>
    public void SetText(string newText)
    {
        text.text = newText;
    }

    /// <summary>
    /// Sets the action to be performed when the popup's button is clicked.
    /// </summary>
    /// <param name="action">The action to be performed.</param>
    public void SetButtonAction(UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    /// <summary>
    /// Shows the popup by setting its visibility and interaction properties.
    /// </summary>
    public void ShowPopup()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Hides the popup by setting its visibility and interaction properties.
    /// </summary>
    public void HidePopup()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
