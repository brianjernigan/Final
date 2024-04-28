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
    [SerializeField] private GameObject _questArrow;
    
    private void OnEnable()
    {
        if (DialogManager.Instance == null) return;
        DialogManager.Instance.OnLevelOneDialogFinished += HandleLevelOneDialogFinished;
    }

    private void OnDisable()
    {
        if (DialogManager.Instance == null) return;
        DialogManager.Instance.OnLevelOneDialogFinished -= HandleLevelOneDialogFinished;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && SceneManager.GetActiveScene().name == "LevelOne")
        {
            DialogManager.Instance.StartDialog(_levelOneScript.lines, DialogType.LevelOneDialog);
        }
    }

    private void HandleLevelOneDialogFinished()
    {
        _questArrow.SetActive(true);
    }
}
