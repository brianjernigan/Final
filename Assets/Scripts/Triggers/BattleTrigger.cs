using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) yield break;
        DialogManager.Instance.StartDialog();
        yield return new WaitUntil(() => DialogManager.Instance.DialogIsFinished);
        BattleManager.Instance.StartBattle();
    }
}
