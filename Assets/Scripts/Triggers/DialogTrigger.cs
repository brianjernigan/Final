using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private LevelData _levelData;
    [SerializeField] private GameObject _questArrow;

    private void OnEnable()
    {
        UIManager.Instance.DialogManager.OnDialogFinished += ActivateQuestArrow;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.DialogManager.StartDialog(_levelData);
            GameManager.Instance.ChangeState(GameState.Dialog);
        }
    }

    private void ActivateQuestArrow()
    {
        _questArrow.SetActive(true);
    }
}
