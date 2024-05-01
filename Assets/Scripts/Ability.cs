using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Ability
{
    protected string Name { get; set; }
    public string Description { get; protected set; }
    public abstract void Activate(ICharacter player, ICharacter target);
}
