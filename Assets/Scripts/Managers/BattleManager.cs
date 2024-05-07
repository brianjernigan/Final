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
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState
{
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
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
    
    public bool BattleIsFinished { get; private set; }
    
    public string PlayerMoveText { get; set; }
    public string EnemyMoveText { get; set; }
    public bool PlayerHasTakenTurn { get; set; }
    public bool EnemyHasTakenTurn { get; set; }

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
        InitializeAbilityPanel();
        UpdateHealthTexts();
        _fpc.enabled = false;
        EnemyHasTakenTurn = false;
        PlayerHasTakenTurn = false;
    }
    
    private void ExitBattleMode()
    {
        EnemyHasTakenTurn = true;
        PlayerHasTakenTurn = true;
        _battlePanel.SetActive(false);
        _fpc.enabled = true;
        GameManager.Instance.ChangeState(GameState.Exploration);
    }

    private void InitializeAbilityPanel()
    {
        UpdateActionText("");
        _battlePanel.SetActive(true);
        ActivateUnlockedAbilityButtons();
        AddButtonListeners();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.A))
        {
            UnlockAllAbilities();
        }
    }

    private void UnlockAllAbilities()
    {
        foreach (var ability in _playerController.Abilities)
        {
            ability.IsUnlocked = true;
        }
    }

    private void ActivateUnlockedAbilityButtons()
    {
        if (_playerController.IsCharging)
        {
            HandleChargingMydoom();
        }
        else
        {
            for (var i = 0; i < _abilityButtons.Length; i++)
            {
                var ability = _playerController.Abilities[i];
                var abilityButton = _abilityButtons[i];
                
                var buttonIsActive = ability.IsUnlocked &&
                                     ability.UsesRemaining > 0;
                
                abilityButton.SetActive(buttonIsActive);
                abilityButton.GetComponent<Button>().interactable = buttonIsActive;
                SetButtonText(abilityButton, i);
            }
        }
    }

    private void HandleChargingMydoom()
    {
        for (var i = 0; i < _abilityButtons.Length; i++)
        {
            var abilityButton = _abilityButtons[i];
            
            abilityButton.SetActive(i == 7);
            if (i == 7)
            {
                abilityButton.GetComponent<Button>().interactable = true;
                abilityButton.GetComponentInChildren<TMP_Text>().text = "Unleash Mydoom";
            }
        }
    }

    private void AddButtonListeners()
    {
        for (var i = 0; i < _abilityButtons.Length; i++)
        {
            var buttonIndex = i;
            var abilityButton = _abilityButtons[buttonIndex];
            
            abilityButton.GetComponent<Button>().onClick.RemoveAllListeners();
            abilityButton.GetComponent<Button>().onClick.AddListener(() => OnAbilityButtonPressed(abilityButton, buttonIndex));
        }
    }

    private void OnAbilityButtonPressed(GameObject button, int buttonIndex)
    {
        _playerController.Abilities[buttonIndex].Activate(_playerController, _enemyController);
        SetButtonText(button, buttonIndex);
        DeactivateButtons();
    }

    private void SetButtonText(GameObject button, int buttonIndex)
    {
        button.GetComponentInChildren<TMP_Text>().text = _playerController.Abilities[buttonIndex].ToString();
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
        BattleIsFinished = false;
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
        }
        
        if (_playerController.IsDead)
        {
            ExitBattleMode();
            GameManager.Instance.ChangeState(GameState.Lost);
            _currentBattleState = BattleState.Lost;
            ResetPlayer();
        } 
        
        if (_enemyController.IsDead)
        {
            ExitBattleMode();
            _currentBattleState = BattleState.Won;
        }
        
        BattleIsFinished = true;
    }

    private void ResetPlayer()
    {
        _playerController.IsDead = false;
        _playerController.CurrentHealth = _playerController.MaxHealth;
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

    public void UpdateHealthTexts()
    {
        UpdateCharacterHealthText(_player);
        UpdateCharacterHealthText(_enemy);
    }

    private void UpdateCharacterHealthText(GameObject affected)
    {
        var characterTextObject = affected == _player ? _playerHealthText : _enemyHealthText;
        var characterName = affected == _player ? _playerController.Name : _enemyController.Name;
        var characterCurrentHealth = affected == _player ? _playerController.CurrentHealth : _enemyController.CurrentHealth;
        characterTextObject.text = $"{characterName} Health: {characterCurrentHealth}";
    }
}
