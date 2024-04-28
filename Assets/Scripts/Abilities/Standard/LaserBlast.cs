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

    public override void Activate(GameObject target)
    {
        Debug.Log("laser beam");
        if (UsesRemaining == 0) return;

        target.GetComponent<Enemy>().TakeDamage(5);

        BattleManager.Instance.PlayerHasTakenTurn = true;
    }
}
