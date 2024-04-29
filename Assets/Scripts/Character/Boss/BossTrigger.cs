using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTrigger : MonoBehaviour
{
    private void OnEnable()
    {
        BattleManager.Instance.OnLevelOneBossWon += HandleLevelOneBossDefeated;
        BattleManager.Instance.OnLevelTwoBossWon += HandleLevelTwoBossDefeated;
        BattleManager.Instance.OnLevelThreeBossWon += HandleLevelThreeBossDefeated;
    }

    private void OnDisable()
    {
        BattleManager.Instance.OnLevelOneBossWon -= HandleLevelOneBossDefeated;
        BattleManager.Instance.OnLevelTwoBossWon -= HandleLevelTwoBossDefeated;
        BattleManager.Instance.OnLevelThreeBossWon -= HandleLevelThreeBossDefeated;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && SceneManager.GetActiveScene().name == "LevelOne")
        {
            GameStateManager.Instance.ChangeState(GameState.Battle);
            BattleManager.Instance.StartBattle(BattleType.LevelOneBoss);
        }
    }

    private void HandleLevelOneBossDefeated()
    {
        throw new NotImplementedException();
    }

    private void HandleLevelTwoBossDefeated()
    {
        throw new NotImplementedException();
    }

    private void HandleLevelThreeBossDefeated()
    {
        throw new NotImplementedException();
    }
}
