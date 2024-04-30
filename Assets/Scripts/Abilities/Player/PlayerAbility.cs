using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : Ability
{
    public int MaxUses;
    public int UsesRemaining;
    public bool IsUnlocked;
    public abstract override void Activate(ICharacter user, ICharacter target);

    public override string ToString()
    {
        return $"{Name} {UsesRemaining}/{MaxUses}";
    }
}
