using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && SceneManager.GetActiveScene().name == "LevelOne")
        {
            GameManager.Instance.ChangeState(GameState.Battle);
            BattleManager.Instance.StartBattle();
        }
    }
}
