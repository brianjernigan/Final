//////////////////////////////////////////////
//Assignment/Lab/Project: Final
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/06/2024
/////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DialogType
{
    Buddy,
    Boss
}

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }
    
    [Header("Panels")]
    [SerializeField] private GameObject _dialogPanel;
    [SerializeField] private TMP_Text _dialogText;
    [SerializeField] private Button _continueButton;

    [Header("Player")] 
    [SerializeField] private GameObject _player;
    [SerializeField] private FirstPersonController _fpc;

    private Queue<string> _sentences;
    private bool _isTyping;
    public bool DialogIsFinished { get; private set; }

    private string _currentSentence;
    private LevelData _currentLevelData;
    private List<string> _currentDialog;

    private DialogType _currentDialogType;

    private int _moveToUnlockIndex;

    private bool _hasSpokenToBuddy;
    
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
        GameManager.Instance.OnLevelLoad += SetLevelData;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelLoad -= SetLevelData;
    }

    private void SetLevelData(LevelData data)
    {
        _currentLevelData = data;
        InitializeDialog();
    }

    private void InitializeDialog()
    {
        _sentences = new Queue<string>();
        _continueButton.onClick.RemoveListener(DisplayNextSentence);
        _continueButton.onClick.AddListener(DisplayNextSentence);
    }

    private void EnterDialogMode(DialogType dialogType)
    {
        GameManager.Instance.ChangeState(GameState.Dialog);
        _dialogPanel.SetActive(true);
        _fpc.enabled = false;
        SetCurrentDialog(dialogType);
    }

    private void SetCurrentDialog(DialogType dialogType)
    {
        if (dialogType == DialogType.Buddy)
        {
            if (_currentLevelData.levelIndex == 2 && _hasSpokenToBuddy)
            {
                _currentDialog = _currentLevelData.dialogs[2].lines;
            }
            else
            {
                _currentDialog = _currentLevelData.dialogs[0].lines;
                if (_currentLevelData.levelIndex == 2)
                {
                    _hasSpokenToBuddy = true;
                }
            }
        }
        else if (dialogType == DialogType.Boss)
        {
            _currentDialog = _currentLevelData.dialogs[1].lines;
        }

        _currentDialogType = dialogType;
    }

    private void ExitDialogMode()
    {
        _dialogPanel.SetActive(false);
        _fpc.enabled = true;
        GameManager.Instance.ChangeState(GameState.Exploration);
    }

    public void StartDialog(DialogType dialogType)
    {
        DialogIsFinished = false;
        EnterDialogMode(dialogType);
        _sentences.Clear();

        foreach (var line in _currentDialog)
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
                HandleDialogFinished();
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
            yield return new WaitForSeconds(0.05f);
        }

        _isTyping = false;
    }

    private void HandleDialogFinished()
    {
        ExitDialogMode();
        if (_currentDialogType == DialogType.Buddy)
        {
            _player.GetComponent<PlayerController>().UnlockAbility(_moveToUnlockIndex++);
        }
        DialogIsFinished = true;
    }
}
