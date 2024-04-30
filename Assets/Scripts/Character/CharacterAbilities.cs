using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour, ICharacter
{
    #region ICharacter Implementation

    public string Name { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsDefending { get; set; }
    public bool IsStunned { get; set; }

    [SerializeField] private List<Ability> _abilities;
    public List<Ability> Abilities
    {
        get => _abilities;
        set => _abilities = value;
    }

    public void PerformAbility(Ability ability, ICharacter user, ICharacter target)
    {
        ability.Activate(user, target);
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
        throw new System.NotImplementedException();
    }

    #endregion
    
    private void OnEnable()
    {
        DialogManager.Instance.OnDialogFinished += HandleDialogFinished;
    }

    private void OnDisable()
    {
        DialogManager.Instance.OnDialogFinished -= HandleDialogFinished;
    }

    private void UnlockAbility(string abilityName)
    {
        var abilityToUnlock = _abilities.Find(ability => ability.Name == abilityName);
        if (abilityToUnlock is PlayerAbility playerAbility)
        {
            playerAbility.IsUnlocked = true;
        }
    }

    private void HandleDialogFinished(string levelName)
    {
        switch (levelName)
        {
            case "Level One":
                UnlockAbility("Electro Pulse");
                break;
        }
    }
}
