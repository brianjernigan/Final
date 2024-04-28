using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ICharacter
{
    public string Name { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsDefending { get; set; }
    
    public void TakeDamage(int amount);
    public void Attack(ICharacter target, int damageAmount);
    public void Defend();
    public void Heal(int amount);
    public void Die();

    public void UpdateHealthText(GameObject textObject);
}
