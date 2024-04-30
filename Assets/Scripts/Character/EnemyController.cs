using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;

    private void InitializeFromData()
    {
        name = _enemyData.EnemyName;
    }
    
    private void Start()
    {
        InitializeFromData();
    }
}
