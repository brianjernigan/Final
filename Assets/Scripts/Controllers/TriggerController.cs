//////////////////////////////////////////////
//Assignment/Lab/Project: Final
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/06/2024
/////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour
{
    private const string BuddyString = "Buddy";
    private const string EnemyString = "Enemy";
    private const string BossString = "Boss";

    [SerializeField] private GameObject[] _objectsToDeactivate;
    [SerializeField] private GameObject[] _objectsToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        var triggerName = gameObject.name;
        
        if (triggerName.Contains(BuddyString))
        {
            StartCoroutine(BuddyRoutine());
        }

        if (triggerName.Contains(EnemyString))
        {
            StartCoroutine(EnemyRoutine());
        }

        if (triggerName.Contains(BossString))
        {
            StartCoroutine(BossRoutine(triggerName));
        }
        
    }

    private IEnumerator BuddyRoutine()
    {
        DialogManager.Instance.StartDialog(DialogType.Buddy);
        yield return new WaitUntil(() => DialogManager.Instance.DialogIsFinished);
        HandleEndOfInteraction();
    }

    private IEnumerator EnemyRoutine()
    {
        BattleManager.Instance.StartBattle(BattleType.Enemy);
        yield return new WaitUntil(() => BattleManager.Instance.BattleIsFinished);
        HandleEndOfInteraction();
    }

    private IEnumerator BossRoutine(string triggerName)
    {
        if (triggerName.Contains("2"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Name = "Tera Nova";
        }
        
        DialogManager.Instance.StartDialog(DialogType.Boss);
        yield return new WaitUntil(() => DialogManager.Instance.DialogIsFinished);
        BattleManager.Instance.StartBattle(BattleType.Boss);
        yield return new WaitUntil(() => BattleManager.Instance.BattleIsFinished);
        HandleEndOfInteraction();
    }

    private void HandleEndOfInteraction()
    {
        ActivateObjects();
        DeactivateObjects();
    }

    private void ActivateObjects()
    {
        if (_objectsToActivate.Length <= 0) return;
        foreach (var obj in _objectsToActivate)
        {
            obj.SetActive(true);
        }
    }

    private void DeactivateObjects()
    {
        if (_objectsToDeactivate.Length <= 0) return;
        foreach (var obj in _objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }
}
