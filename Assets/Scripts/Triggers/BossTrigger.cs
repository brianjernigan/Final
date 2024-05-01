using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && SceneManager.GetActiveScene().name == "LevelOne")
        {
            GameManager.Instance.ChangeState(GameState.Battle);
            BattleManager.Instance.StartBattle();
        }
    }
}
