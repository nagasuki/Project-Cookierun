using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthViewModel : MonoBehaviour
{
    public CharacterHealthModel characterHealthModel;

    public int CurrentHealth => characterHealthModel.CurrentHealth;
    public int MaxHealth => characterHealthModel.MaxHealth;

    private void OnEnable()
    {
        characterHealthModel = new CharacterHealthModel(2);

        characterHealthModel.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        characterHealthModel.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int newHealth)
    {
        Debug.Log("Player health updated to: " + newHealth);
    }

    public void TakeDamage(int damage)
    {
        characterHealthModel.TakeDamage(damage);
    }

    public void Heal(int amount)
    {
        characterHealthModel.Heal(amount);
    }
}
