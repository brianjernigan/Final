using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _levelOneDoor;
    
    private void OnEnable()
    {
        BattleManager.Instance.OnLevelOneBattleWon += HandleLevelOneBattleWon;
        BattleManager.Instance.OnLevelTwoBattleWon += HandleLevelTwoBattleWon;
        BattleManager.Instance.OnLevelThreeBattleWon += HandleLevelThreeBattleWon;
    }

    private void OnDisable()
    {
        BattleManager.Instance.OnLevelOneBattleWon -= HandleLevelOneBattleWon;
        BattleManager.Instance.OnLevelTwoBattleWon -= HandleLevelTwoBattleWon;
        BattleManager.Instance.OnLevelThreeBattleWon -= HandleLevelThreeBattleWon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && SceneManager.GetActiveScene().name == "LevelOne")
        {
            GameStateManager.Instance.ChangeState(GameState.Battle);
            BattleManager.Instance.StartBattle(BattleType.LevelOneBattle);
        }
    }

    private void HandleLevelOneBattleWon()
    {
        _levelOneDoor.SetActive(false);
        GameStateManager.Instance.ChangeState(GameState.Exploration);
    }

    private void HandleLevelTwoBattleWon()
    {
        throw new NotImplementedException();
    }

    private void HandleLevelThreeBattleWon()
    {
        throw new NotImplementedException();
    }
}
