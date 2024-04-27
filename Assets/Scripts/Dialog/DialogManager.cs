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
    [SerializeField] private GameObject _hud;

    [Header("Player")] 
    [SerializeField] private GameObject _player;
    private LaserShooter _ls;
    private FirstPersonController _fpc;

    private Queue<string> _sentences;
    private bool _isTyping;

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
        _ls = _player.GetComponent<LaserShooter>();
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
        _hud.SetActive(false);
        _ls.enabled = false;
        _fpc.enabled = false;
    }

    private void ExitDialogMode()
    {
        _dialogPanel.SetActive(false);
        _hud.SetActive(true);
        _ls.enabled = true;
        _fpc.enabled = true;
    }

    public void StartDialog(List<string> script)
    {
        EnterDialogMode();
        _sentences.Clear();

        foreach (var line in script)
        {
            _sentences.Enqueue(line);
        }
        
        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            ExitDialogMode();
            return;
        }
        
        if (_isTyping)
        {
            StopAllCoroutines();
            _dialogText.text = _sentences.Peek();
            _isTyping = false;
            return;
        }

        StartCoroutine(TypeSentence(_sentences.Dequeue()));
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
}
