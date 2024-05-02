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

    private void EnterBattleMode(BattleType battleType)
    {
        GameManager.Instance.ChangeState(GameState.Battle);
        SetCurrentEnemy(battleType);
        UpdateActionText("");
        _battlePanel.SetActive(true);
        UpdateHealthText(_enemy);
        _fpc.enabled = false;
        BattleIsFinished = false;
    }

    private void SetCurrentEnemy(BattleType battleType)
    {
        switch (battleType)
        {
            case BattleType.Enemy:
                _enemy = GameObject.Find(_currentLevelData.npcNames[1]);
                break;
            case BattleType.Boss:
                _enemy = GameObject.Find(_currentLevelData.npcNames[2]);
                break;
        }
        
        _enemyController = _enemy.GetComponent<EnemyController>();
    }

    private void ExitBattleMode()
    {
        _battlePanel.SetActive(false);
        _fpc.enabled = true;
        BattleIsFinished = true;
        GameManager.Instance.ChangeState(GameState.Exploration);
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
        if (_playerController.Abilities[buttonIndex] is not PlayerAbility ability) throw new NullReferenceException("Not a player ability");

        var buttonIsActive = ability.IsUnlocked && ability.UsesRemaining > 0;
        _abilityButtons[buttonIndex].SetActive(buttonIsActive);
        _abilityButtons[buttonIndex].GetComponent<Button>().onClick.AddListener(() => ability.Activate(_playerController, _enemyController));
        _abilityButtons[buttonIndex].GetComponent<Button>().onClick.AddListener(() => UpdateButtonText(_abilityButtons[buttonIndex], ability));
        _abilityButtons[buttonIndex].GetComponentInChildren<TMP_Text>().text = ability.ToString();
    }

    private void UpdateButtonText(GameObject button, Ability ability)
    {
        button.GetComponentInChildren<TMP_Text>().text = ability.ToString();
    }
    
    public void StartBattle(BattleType battleType)
    {
        EnterBattleMode(battleType);
        PopulateAbilityButtons();
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
        ActivateButtons();
        yield return new WaitUntil(() => PlayerHasTakenTurn);
        UpdateActionText(PlayerMoveText);
        _currentBattleState = BattleState.EnemyTurn;
    }

    private IEnumerator EnemyTurnRoutine()
    {
        EnemyHasTakenTurn = false;
        DeactivateButtons();
        yield return new WaitForSeconds(1.25f);
        StartCoroutine(_enemyController.TakeTurn());
        yield return new WaitUntil(() => EnemyHasTakenTurn);
        UpdateActionText(EnemyMoveText); 
        _currentBattleState = BattleState.PlayerTurn;
    }

    public void UpdateActionText(string actionText)
    {
        _updateText.text = actionText;
    }

    public void UpdateHealthText(GameObject affected)
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
