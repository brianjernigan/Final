using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlast : Ability
{
    public LaserBlast() : base("Laser Blast", 20)
    {
        IsUnlocked = true;
    }

    public override void Activate(ICharacter player, ICharacter target)
    {
        player.IsDefending = false;
        
        Debug.Log("laser blast");
        if (UsesRemaining == 0) return;

        player.Attack(target, 10);

        EndTurn();
    }
}
