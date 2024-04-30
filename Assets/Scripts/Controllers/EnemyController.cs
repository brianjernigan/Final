using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacter
{
    #region ICharacter Implementation

    public string Name { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsDefending { get; set; }
    public bool IsStunned { get; set; }
    public bool IsConfused { get; set; }

    [SerializeField] private List<EnemyAbility> _abilities;
    public List<EnemyAbility> Abilities
    {
        get => _abilities;
        set => _abilities = value;
    }

    public void TakeDamage(int amount)
    {
        if (IsDefending)
        {
            amount /= 2;
            IsDefending = false;
        }

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
    }

    public void ApplyStatusEffect()
    {
        throw new NotImplementedException();
    }

    public void EndTurn()
    {
        throw new NotImplementedException();
    }

    #endregion
}
