using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;

    public int MaxHealth { get; set; } = 100;
    public int CurrentHealth { get; set; }
    public bool IsDefending { get; set; }
    public bool IsDead { get; set; }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        UpdateHealthText();
    }
    
    public void TakeDamage(int amount)
    {
        if (IsDefending)
        {
            amount /= 2;
            IsDefending = false;
        }
        
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        UpdateHealthText();
        
        if (CurrentHealth <= 0)
        {
            IsDead = true;
        }
    }

    private void UpdateHealthText()
    {
        _healthText.text = $"Player Health: {CurrentHealth}";
    }
}
