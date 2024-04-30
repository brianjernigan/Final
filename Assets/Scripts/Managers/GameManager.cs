using System;
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

public enum Level
{
    One,
    Two,
    Three
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GameState CurrentState { get; set; }
    public LevelData CurrentLevelData { get; private set; }
    
    [Header("Level Data")]
    [SerializeField] private LevelData _levelOneData;
    [SerializeField] private LevelData _levelTwoData;
    [SerializeField] private LevelData _levelThreeData;

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

    private void OnEnable()
    {
        UIManager.Instance.DialogManager.OnDialogFinished += OnStateChange;
    }

    private void Start()
    {
        ChangeState(GameState.Exploration);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        OnStateChange();
    }

    private void OnStateChange()
    {
        switch (CurrentState)
        {
            case GameState.Exploration:
                HandleMouseBehavior(CurrentState);
                break;
            case GameState.Dialog:
                HandleMouseBehavior(CurrentState);
                break;
            case GameState.Battle:
                HandleMouseBehavior(CurrentState);
                break;
        }
    }

    private void HandleMouseBehavior(GameState currentState)
    {
        switch (currentState)
        {
            case GameState.Dialog or GameState.Battle:
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
            case GameState.Exploration:
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
                break;
        }
    }
    
    public void LoadLevel(Level levelNumber)
    {
        switch (levelNumber)
        {
            case Level.One:
                CurrentLevelData = _levelOneData;
                break;
            case Level.Two:
                CurrentLevelData = _levelTwoData;
                break;
            case Level.Three:
                CurrentLevelData = _levelThreeData;
                break;
        }
    }
}
