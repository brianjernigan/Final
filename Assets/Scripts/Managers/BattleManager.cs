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

public enum BattleType
{
    Enemy,
    Boss
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
    [SerializeField] private TMP_Text _playerHealthText;
    
    [Header("Enemy")]
    private GameObject _enemy;
    private EnemyController _enemyController;
    [SerializeField] private TMP_Text _enemyHealthText;
    
    private BattleState _currentBattleState;
    
    public bool BattleIsFinished { get; set; }

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
    }
    
    private void SetCurrentEnemy(BattleType battleType)
    {
        _enemy = battleType switch
        {
            BattleType.Enemy => GameObject.Find(_currentLevelData.npcNames[1]),
            BattleType.Boss => GameObject.Find(_currentLevelData.npcNames[2]),
            _ => _enemy
        };

        _enemyController = _enemy.GetComponent<EnemyController>();
    }

    private void EnterBattleMode(BattleType battleType)
    {
        GameManager.Instance.ChangeState(GameState.Battle);
        SetCurrentEnemy(battleType);
        UpdateActionText("");
        _battlePanel.SetActive(true);
        InitializeAbilityPanel();
        UpdateCharacterHealthText(_enemy);
        _fpc.enabled = false;
        BattleIsFinished = false;
    }
    
    private void ExitBattleMode()
    {
        _battlePanel.SetActive(false);
        _fpc.enabled = true;
        BattleIsFinished = true;
        GameManager.Instance.ChangeState(GameState.Exploration);
    }

    private void InitializeAbilityPanel()
    {
        ActivateUnlockedAbilityButtons();
        AddButtonListeners();
    }

    private void ActivateUnlockedAbilityButtons()
    {
        if (_playerController.IsCharging)
        {
            HandleChargingMydoom();
        }
        else
        {
            for (int i = 0; i < _abilityButtons.Length; i++)
            {
                var buttonIsActive = _playerController.Abilities[i].IsUnlocked &&
                                     _playerController.Abilities[i].UsesRemaining > 0;
                _abilityButtons[i].SetActive(true);
                _abilityButtons[i].GetComponent<Button>().interactable = true;
                _abilityButtons[i].GetComponentInChildren<TMP_Text>().text = _playerController.Abilities[i].ToString();
            }
        }
    }

    private void HandleChargingMydoom()
    {
        for (int i = 0; i < _abilityButtons.Length; i++)
        {
            _abilityButtons[i].SetActive(i == 7);
            if (i == 7)
            {
                _abilityButtons[i].GetComponent<Button>().interactable = true;
                _abilityButtons[i].GetComponentInChildren<TMP_Text>().text = "Unleash Mydoom";
            }
        }
    }

    private void AddButtonListeners()
    {
        for (int i = 0; i < _abilityButtons.Length; i++)
        {
            var buttonIndex = i;
            var button = _abilityButtons[buttonIndex];
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(() => OnAbilityButtonPressed(button, buttonIndex));
        }
    }

    private void OnAbilityButtonPressed(GameObject button, int buttonIndex)
    {
        _playerController.Abilities[buttonIndex].Activate(_playerController, _enemyController);
        button.GetComponentInChildren<TMP_Text>().text = _playerController.Abilities[buttonIndex].ToString();
        DeactivateButtons();
    }

    private void DeactivateButtons()
    {
        foreach (var button in _abilityButtons)
        {
            button.GetComponent<Button>().interactable = false;
        }
    }
    
    public void StartBattle(BattleType battleType)
    {
        EnterBattleMode(battleType);
        _currentBattleState = BattleState.PlayerTurn;
        StartCoroutine(BattleRoutine());
    }

    private IEnumerator BattleRoutine()
    {
        while (!_playerController.IsDead && !_enemyController.IsDead)
        {
            switch (_currentBattleState)
            {
                case BattleState.PlayerTurn:
                    yield return StartCoroutine(PlayerTurnRoutine());
                    break;
                case BattleState.EnemyTurn:
                    yield return StartCoroutine(EnemyTurnRoutine());
                    break;
            }
            
            if (_playerController.IsDead)
            {
                // Lose
                ExitBattleMode();
            } 
        
            if (_enemyController.IsDead)
            {
                // Win
                ExitBattleMode();
            }
        }
    }

    private IEnumerator PlayerTurnRoutine()
    {
        PlayerHasTakenTurn = false;
        ActivateUnlockedAbilityButtons();
        yield return new WaitUntil(() => PlayerHasTakenTurn);
        UpdateActionText(PlayerMoveText);
        UpdateHealthTexts();
        _currentBattleState = BattleState.EnemyTurn;
    }

    private IEnumerator EnemyTurnRoutine()
    {
        EnemyHasTakenTurn = false;
        StartCoroutine(_enemyController.TakeTurn());
        yield return new WaitUntil(() => EnemyHasTakenTurn);
        UpdateActionText(EnemyMoveText);
        UpdateHealthTexts();
        _currentBattleState = BattleState.PlayerTurn;
    }

    public void UpdateActionText(string actionText)
    {
        _updateText.text = actionText;
    }

    private void UpdateHealthTexts()
    {
        UpdateCharacterHealthText(_player);
        UpdateCharacterHealthText(_enemy);
    }

    private void UpdateCharacterHealthText(GameObject affected)
    {
        TMP_Text textObject;
        
        if (affected == _player)
        {
            textObject = _playerHealthText;
            textObject.text = $"{_playerController.Name} Health: {_playerController.CurrentHealth}";
        } 
        else if (affected == _enemy)
        {
            textObject = _enemyHealthText;
            textObject.text = $"{_enemyController.Name} Health: {_enemyController.CurrentHealth}";
        }
    }
}
