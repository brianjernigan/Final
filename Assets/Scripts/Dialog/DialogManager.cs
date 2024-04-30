using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }
    
    [Header("Panels")]
    [SerializeField] private GameObject _dialogPanel;
    [SerializeField] private TMP_Text _dialogText;
    [SerializeField] private Button _continueButton;

    [Header("Player")] 
    [SerializeField] private GameObject _player;
    private FirstPersonController _fpc;

    private Queue<string> _sentences;
    private bool _isTyping;

    private LevelData _levelData;
    
    private string _currentSentence;

    public delegate void DialogFinishedDelegate(string levelName);
    public event DialogFinishedDelegate OnDialogFinished;

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

    private void Initialize()
    {
        _sentences = new Queue<string>();
        _fpc = _player.GetComponent<FirstPersonController>();
    }
    
    private void Start()
    {
        Initialize();
        _continueButton.onClick.AddListener(DisplayNextSentence);
    }

    private void EnterDialogMode()
    {
        _dialogPanel.SetActive(true);
        _fpc.enabled = false;
    }

    private void ExitDialogMode()
    {
        _dialogPanel.SetActive(false);
        _fpc.enabled = true;
        GameStateManager.Instance.ChangeState(GameState.Exploration);
    }

    public void StartDialog(LevelData levelData)
    {
        _levelData = levelData;
        EnterDialogMode();
        _sentences.Clear();

        foreach (var line in _levelData.dialogData.Lines)
        {
            _sentences.Enqueue(line);
        }
        
        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        _currentSentence = _sentences.Peek();
        
        if (_isTyping)
        {
            StopAllCoroutines();
            _dialogText.text = _currentSentence;
            _isTyping = false;
        }
        else if (_dialogText.text == _currentSentence)
        {
            _sentences.Dequeue();
            
            if (_sentences.Count == 0)
            {
                ExitDialogMode();
                TriggerDialogFinishedEvent(_levelData.LevelName);
            }
            else
            {
                StartCoroutine(TypeSentence(_sentences.Peek()));
            }
        }
        else
        {
            StartCoroutine(TypeSentence(_sentences.Peek()));
        }
    }
    
    private IEnumerator TypeSentence(string sentence)
    {
        _dialogText.text = "";
        _isTyping = true;

        foreach (var letter in sentence.ToCharArray())
        {
            _dialogText.text += letter;
            yield return new WaitForSeconds(0.075f);
        }

        _isTyping = false;
    }

    private void TriggerDialogFinishedEvent(string levelName)
    {
        OnDialogFinished?.Invoke(levelName);
    }
}
