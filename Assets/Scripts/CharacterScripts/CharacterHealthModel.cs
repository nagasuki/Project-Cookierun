using UnityEngine;

public class CharacterHealthModel
{
    private int _currentHealth;
    public int MaxHealth { get; private set; }

    public delegate void HealthChanged(int currentHealth);
    public event HealthChanged OnHealthChanged;

    public CharacterHealthModel(int maxHealth)
    {
        MaxHealth = maxHealth;
        _currentHealth = maxHealth;
    }

    public int CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
    }
}
