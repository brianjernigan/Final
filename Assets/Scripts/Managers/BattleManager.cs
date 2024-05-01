using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState
{
    PlayerTurn,
    EnemyTurn,
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [Header("UI")] 
    [SerializeField] private GameObject _battlePanel;
    [SerializeField] private GameObject[] _abilityButtons;
    [SerializeField] private TMP_Text _updateText;
    
    [Header("Player")]
    [SerializeField] private GameObject _player;
    private PlayerController _playerController;
    private FirstPersonController _fpc;
    
    private GameObject _enemy;
    private EnemyController _enemyController;
    
    private BattleState _currentBattleState;

    public delegate void BattleFinishedDelegate(bool result);
    public event BattleFinishedDelegate OnBattleFinished;
    
    public string PlayerMoveText { get; set; }
    public string EnemyMoveText { get; set; }
    public bool PlayerHasTakenTurn { get; set; }
    public bool EnemyHasTakenTurn { get; set; }

    private bool _buttonsInitialized;

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
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        _playerController = _player.GetComponent<PlayerController>();
        _fpc = _player.GetComponent<FirstPersonController>();
        _enemy = GameObject.Find(_currentLevelData.interactableNames[1]);
        _enemyController = _enemy.GetComponent<EnemyController>();
    }

    private void EnterBattleMode()
    {
        _battlePanel.SetActive(true);
        _fpc.enabled = false;
    }

    private void ExitBattleMode()
    {
        _battlePanel.SetActive(false);
        _fpc.enabled = true;
    }

    private void DeactivateButtons()
    {
        foreach (var button in _abilityButtons)
        {
            button.GetComponent<Button>().interactable = false;
        }
    }

    private void ActivateButtons()
    {
        foreach (var button in _abilityButtons)
        {
            button.GetComponent<Button>().interactable = true;
        }
    }

    private void PopulateAbilityButtons()
    {
        if (_buttonsInitialized) return;
        
        for (var i = 0; i < _abilityButtons.Length; i++)
        {
            InitializeButtons(i);
        }

        _buttonsInitialized = true;
    }

    private void InitializeButtons(int buttonIndex)
    {
        if (PlayerController.Instance.Abilities[buttonIndex] is not PlayerAbility ability) throw new NullReferenceException("Not a player ability");
        
        _abilityButtons[buttonIndex].SetActive(ability.IsUnlocked);
        _abilityButtons[buttonIndex].GetComponent<Button>().onClick.AddListener(() => ability.Activate(_playerController, _enemyController));
        _abilityButtons[buttonIndex].GetComponent<Button>().onClick.AddListener(() => UpdateButtonText(_abilityButtons[buttonIndex], ability));
        _abilityButtons[buttonIndex].GetComponentInChildren<TMP_Text>().text = ability.ToString();
    }

    private void UpdateButtonText(GameObject button, Ability ability)
    {
        button.GetComponentInChildren<TMP_Text>().text = ability.ToString();
    }
    
    public void StartBattle()
    {
        EnterBattleMode();
        PopulateAbilityButtons();
        _currentBattleState = BattleState.PlayerTurn;
        StartCoroutine(BattleRoutine());
    }

    private IEnumerator BattleRoutine()
    {
        while (!PlayerController.Instance.IsDead && !_enemyController.IsDead)
        {
            if (_currentBattleState == BattleState.PlayerTurn)
            {
                PlayerHasTakenTurn = false;
                ActivateButtons();
                yield return new WaitUntil(() => PlayerHasTakenTurn);
                UpdateActionText(PlayerMoveText);
                _currentBattleState = BattleState.EnemyTurn;
            } else if (_currentBattleState == BattleState.EnemyTurn)
            {
                EnemyHasTakenTurn = false;
                DeactivateButtons();
                yield return new WaitForSeconds(2.5f);
                _enemyController.TakeTurn();
                yield return new WaitUntil(() => EnemyHasTakenTurn);
                UpdateActionText(EnemyMoveText); 
                _currentBattleState = BattleState.PlayerTurn;
            }
        }

        if (PlayerController.Instance.IsDead)
        {
            ExitBattleMode();
        } else if (_enemyController.IsDead)
        {
            ExitBattleMode();
        }

        yield return null;
    }

    private void UpdateActionText(string actionText)
    {
        _updateText.text = actionText;
    }
}
