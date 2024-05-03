using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    [SerializeField] private CharacterController _controller;

    [Header("Transition")] 
    [SerializeField] private Image _panelImage;
    private readonly float _fadeDuration = 2;
    
    public GameState CurrentState { get; set; }
    [SerializeField] private LevelData[] _levelData;
    public delegate void LevelLoadDelegate(LevelData data);
    public event LevelLoadDelegate OnLevelLoad;
    private LevelData _currentLevelData;

    private bool _fadeIsFinished;
    
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
                    LoadLevel(_currentLevelData.levelIndex + 1);
                    ChangeState(GameState.Exploration);
                }
                else
                {
                    ChangeState(GameState.Won);
                }
                break;
            case GameState.Won:
                HandleMouseBehavior(state);
                WinGame();
                break;
            case GameState.Lost:
                HandleMouseBehavior(state);
                LoadLevel(_currentLevelData.levelIndex);
                break;
        }
    }

    private void HandleMouseBehavior(GameState currentState)
    {
        switch (currentState)
        {
            case GameState.Dialog or GameState.Battle or GameState.Won:
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
            case GameState.Exploration:
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
                break;
            case GameState.NextLevel or GameState.Lost:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }

    private void WinGame()
    {
        SceneManager.LoadScene("YouWin");
    }

    private void LoadLevel(int levelIndex)
    {
        if (levelIndex != 0)
        {
            StartCoroutine(Transition(levelIndex));
        }
        InitializeLevel(levelIndex);
    }

    private void InitializeLevel(int levelIndex)
    {
        _currentLevelData = _levelData[levelIndex];
        OnLevelLoad?.Invoke(_currentLevelData);
    }
    
    private IEnumerator Transition(int levelIndex)
    {
        _controller.enabled = false;

        yield return StartCoroutine(Fade(1));
        yield return new WaitUntil(() => _fadeIsFinished);

        ResetPlayerPosition();

        var asyncLoad = SceneManager.LoadSceneAsync($"Level{levelIndex}");
        while (asyncLoad is { isDone: false })
        {
            yield return null;
        }

        yield return StartCoroutine(Fade(0));
        yield return new WaitUntil(() => _fadeIsFinished);

        _controller.enabled = true;
    }

    private void ResetPlayerPosition()
    {
        var resetPosition = new Vector3(6, 0, -8);
        _player.transform.position = resetPosition;
        _controller.Move(Vector3.zero);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        _fadeIsFinished = false;
        var alpha = _panelImage.color.a;

        while (!Mathf.Approximately(alpha, targetAlpha))
        {
            alpha = Mathf.MoveTowards(alpha, targetAlpha, Time.deltaTime / _fadeDuration);
            _panelImage.color = new Color(_panelImage.color.r, _panelImage.color.g, _panelImage.color.b, alpha);
            yield return null;
        }

        _fadeIsFinished = true;
    }
}
