using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ICharacter
{
    string Name { get; set; }
    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }
    bool IsDead { get; set; }
    bool IsDefending { get; set; }
    bool IsStunned { get; set; }
    bool IsConfused { get; set; }
    bool IsSwarmed { get; set; }
    bool IsHiding { get; set; }
    bool IsCharging { get; set; }
    
    void TakeDamage(int amount);
    void ApplyStatusEffect();
}
