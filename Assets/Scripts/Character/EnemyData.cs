using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    public int MaxHealth;
    public int CurrentHealth;
    public Sprite Sprite;
    public List<Ability> Abilities;
}
