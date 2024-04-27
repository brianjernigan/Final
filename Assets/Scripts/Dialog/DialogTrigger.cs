using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private DialogScripts _dialogScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DialogManager.Instance.StartDialog(_dialogScript.sentences);
        }
    }
}
