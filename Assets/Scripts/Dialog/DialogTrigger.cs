using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private DialogScriptableObject _dialogScript;

    private void OnEnable()
    {
        if (DialogManager.Instance != null)
        {
            DialogManager.Instance.OnLevelOneDialogFinished += HandleLevelOneDialogFinished;
        }
    }

    private void OnDisable()
    {
        if (DialogManager.Instance != null)
        {
            DialogManager.Instance.OnLevelOneDialogFinished -= HandleLevelOneDialogFinished;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && SceneManager.GetActiveScene().name == "LevelOne")
        {
            DialogManager.Instance.StartDialog(_dialogScript.lines, DialogType.LevelOneDialog);
        }
    }

    private void HandleLevelOneDialogFinished()
    {
        Debug.Log("level one dialog finished");
    }
}
