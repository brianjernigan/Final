using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private DialogScriptableObject _dialogScript;

    private DialogLines _currentDialogData;

    private void Awake()
    {
        _currentDialogData = new DialogLines()
        { 
            lines = new List<string>(_dialogScript.dialogData.lines)
        };
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DialogManager.Instance.StartDialog(_currentDialogData.lines);
        }
    }
}
