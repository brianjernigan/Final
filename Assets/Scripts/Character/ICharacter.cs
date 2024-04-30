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
    List<Ability> Abilities { get; set; }

    public void PerformAbility(Ability ability, ICharacter user, ICharacter target);
    void TakeDamage(int amount);
    void ApplyStatusEffect();
}
