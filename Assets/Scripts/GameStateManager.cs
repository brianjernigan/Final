using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public enum GameState
{
    Exploration,
    Dialog,
    Battle
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    
    public GameState CurrentState { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.Exploration);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        OnStateChange(newState);
    }

    private void OnStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Exploration:
                Debug.Log("exploration");
                break;
            case GameState.Dialog:
                Debug.Log("dialog");
                break;
            case GameState.Battle:
                Debug.Log("battle");
                break;
        }
    }
}
