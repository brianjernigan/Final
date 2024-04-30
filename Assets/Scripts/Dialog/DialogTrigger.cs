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
        DialogManager.Instance.OnDialogFinished += ActivateQuestArrow;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogManager.Instance.StartDialog(_levelData);
            GameStateManager.Instance.ChangeState(GameState.Dialog);
        }
    }

    private void ActivateQuestArrow(string ignore)
    {
        _questArrow.SetActive(true);
    }
}
