using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Ability
{
    public virtual string Name { get; set; }
    public string Description { get; protected set; }
    internal abstract void Activate(ICharacter player, ICharacter target);
    public abstract void EndTurn(string turnText);
}
