using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public enum GameState
{
    Exploration,
    Dialog,
    Battle,
    NextLevel,
    Won,
    Lost
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _player;
    public GameState CurrentState { get; set; }
    [SerializeField] private LevelData[] _levelData;
    public delegate void LevelLoadDelegate(LevelData data);
    public event LevelLoadDelegate OnLevelLoad;

    private LevelData _currentLevelData;
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
        LoadLevel(0);
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
                HandleMouseBehavior(state);
                break;
            case GameState.Dialog:
                HandleMouseBehavior(state);
                break;
            case GameState.Battle:
                HandleMouseBehavior(state);
                break;
            case GameState.NextLevel:
                HandleMouseBehavior(state);
                if (_currentLevelData.levelIndex <= 1)
                {
                    SceneManager.LoadScene($"Level{_currentLevelData.levelIndex + 1}");
                    LoadLevel(_currentLevelData.levelIndex + 1);
                    ChangeState(GameState.Exploration);
                }
                else
                {
                    ChangeState(GameState.Won);
                    // Handle Game Won
                }
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
            case GameState.NextLevel or GameState.Won or GameState.Lost:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }

    private void LoadLevel(int index)
    {
        _currentLevelData = _levelData[index];
        ResetPlayerPosition();
        OnLevelLoad?.Invoke(_currentLevelData);
    }

    private void ResetPlayerPosition()
    {
        var resetPosition = new Vector3(6, 0, -8);
        var controller = _player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            _player.transform.position = resetPosition;
            controller.enabled = true;
        }
        else
        {
            _player.transform.position = resetPosition;
        }

        if (controller != null)
        {
            controller.Move(Vector3.up);
        }
    }
}
