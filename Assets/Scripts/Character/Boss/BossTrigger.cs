using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameStateManager.Instance.ChangeState(GameState.Battle);
            BattleManager.Instance.StartBattle();
        }
    }
}
