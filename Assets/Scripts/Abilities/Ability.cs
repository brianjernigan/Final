using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public int MaxUses { get; protected set; }
    public int UsesRemaining { get; set; }
    public bool IsUnlocked { get; set; }
    public abstract void Activate(GameObject target);

    protected Ability(string name, int maxUses)
    {
        Name = name;
        MaxUses = maxUses;
        UsesRemaining = MaxUses;
    }

    public override string ToString()
    {
        return $"{Name}: {UsesRemaining}/{MaxUses}";
    }
}
