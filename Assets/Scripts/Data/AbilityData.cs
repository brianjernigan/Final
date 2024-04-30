using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    public string Name;
    public Sprite Icon;

    public abstract void Activate(ICharacter user, ICharacter target);
}
