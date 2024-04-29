using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private DialogScriptableObject _levelOneScript;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _questArrowOne;
    
    private void OnEnable()
    {
        DialogManager.Instance.OnLevelOneDialogFinished += HandleLevelOneDialogFinished;
        DialogManager.Instance.OnLevelTwoDialogFinished += HandleLevelTwoDialogFinished;
        DialogManager.Instance.OnLevelThreeDialogFinished += HandleLevelThreeDialogFinished;
    }

    private void OnDisable()
    {
        DialogManager.Instance.OnLevelOneDialogFinished -= HandleLevelOneDialogFinished;
        DialogManager.Instance.OnLevelTwoDialogFinished -= HandleLevelTwoDialogFinished;
        DialogManager.Instance.OnLevelThreeDialogFinished -= HandleLevelThreeDialogFinished;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && SceneManager.GetActiveScene().name == "LevelOne")
        {
            GameStateManager.Instance.ChangeState(GameState.Dialog);
            DialogManager.Instance.StartDialog(_levelOneScript.lines, DialogType.LevelOneDialog);
        }
    }

    private void HandleLevelOneDialogFinished()
    {
        _questArrowOne.SetActive(true);
        Player.Instance.UnlockAbility(1);
        GameStateManager.Instance.ChangeState(GameState.Exploration);
    }

    private void HandleLevelTwoDialogFinished()
    {
        throw new NotImplementedException();
    }

    private void HandleLevelThreeDialogFinished()
    {
        throw new NotImplementedException();
    }
}
