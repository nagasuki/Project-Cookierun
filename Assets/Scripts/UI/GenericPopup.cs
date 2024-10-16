using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GenericPopup : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;

    private void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }

    public void SetButtonAction(UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    public void ShowPopup()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void HidePopup()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
