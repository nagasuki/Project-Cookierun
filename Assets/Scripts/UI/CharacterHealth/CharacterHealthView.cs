using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealthView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private CharacterHealthViewModel viewModel;

    void Start()
    {
        healthText.text = "X" + viewModel.MaxHealth.ToString();

        viewModel.characterHealthModel.OnHealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar(int newHealth)
    {
        healthText.text = "X" + newHealth.ToString();
    }
}
