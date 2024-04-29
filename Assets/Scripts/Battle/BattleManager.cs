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
    LevelOneBattle,
    LevelOneBoss,
    LevelTwoBattle,
    LevelTwoBoss,
    LevelThreeBattle,
    LevelThreeBoss,
    Lost
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
    private FirstPersonController _fpc;

    [Header("Enemy")] 
    [SerializeField] private GameObject _enemy;
    private Enemy _enemyComponent;
    
    private BattleState _currentBattleState;
    
    public string PlayerMoveName { get; set; }
    public string EnemyMoveName { get; set; }
    public bool PlayerHasTakenTurn { get; set; }
    public bool EnemyHasTakenTurn { get; set; }

    private BattleType _currentBattle;
    
    public event Action OnLevelOneBattleWon;
    public event Action OnLevelOneBossWon;
    public event Action OnLevelTwoBattleWon;
    public event Action OnLevelTwoBossWon;
    public event Action OnLevelThreeBattleWon;
    public event Action OnLevelThreeBossWon;
    public event Action OnBattleLost;

    private bool _buttonsInitialized;
    
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
        Initialize();
    }

    private void Initialize()
    {
        _fpc = _player.GetComponent<FirstPersonController>();
        _enemyComponent = _enemy.GetComponent<Enemy>();
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
        _abilityButtons[buttonIndex].SetActive(Player.Instance.Abilities[buttonIndex].IsUnlocked);
        _abilityButtons[buttonIndex].GetComponent<Button>().onClick.AddListener(() => Player.Instance.Abilities[buttonIndex].Activate(_player.GetComponent<Player>(), _enemy.GetComponent<Enemy>()));
        _abilityButtons[buttonIndex].GetComponent<Button>().onClick.AddListener(() => UpdateButtonText(_abilityButtons[buttonIndex], Player.Instance.Abilities[buttonIndex]));
        _abilityButtons[buttonIndex].GetComponentInChildren<TMP_Text>().text = Player.Instance.Abilities[buttonIndex].ToString();
    }

    private void UpdateButtonText(GameObject button, Ability ability)
    {
        button.GetComponentInChildren<TMP_Text>().text = ability.ToString();
    }
    
    public void StartBattle(BattleType levelBattle)
    {
        EnterBattleMode();
        PopulateAbilityButtons();
        _currentBattleState = BattleState.PlayerTurn;
        _currentBattle = levelBattle;
        StartCoroutine(BattleRoutine(levelBattle));
    }

    private IEnumerator BattleRoutine(BattleType levelBattle)
    {
        while (!Player.Instance.IsDead && !_enemyComponent.IsDead)
        {
            if (_currentBattleState == BattleState.PlayerTurn)
            {
                Debug.Log("Player's Turn");
                PlayerHasTakenTurn = false;
                ActivateButtons();
                yield return new WaitUntil(() => PlayerHasTakenTurn);
                UpdateActionText(_player, PlayerMoveName);
                _currentBattleState = BattleState.EnemyTurn;
            } else if (_currentBattleState == BattleState.EnemyTurn)
            {
                Debug.Log("Enemy's turn");
                EnemyHasTakenTurn = false;
                DeactivateButtons();
                yield return new WaitForSeconds(2.5f);
                _enemyComponent.TakeTurn();
                yield return new WaitUntil(() => EnemyHasTakenTurn);
                UpdateActionText(_enemy, EnemyMoveName); 
                _currentBattleState = BattleState.PlayerTurn;
            }
        }

        if (Player.Instance.IsDead)
        {
            Debug.Log("Player loses");
            TriggerBattleFinishedEvent(BattleType.Lost);
            ExitBattleMode();
        } else if (_enemyComponent.IsDead)
        {
            Debug.Log("Player win");
            TriggerBattleFinishedEvent(levelBattle);
            ExitBattleMode();
        }

        yield return null;
    }

    private void UpdateActionText(GameObject turnTaker, string action)
    {
        var turnTakerString = "";

        if (turnTaker == _player)
        {
            turnTakerString = "Player";
        } else if (turnTaker == _enemy)
        {
            turnTakerString = "Enemy";
        }

        _updateText.text = $"{turnTakerString} used: {action}!";
    }

    private void TriggerBattleFinishedEvent(BattleType levelBattle)
    {
        switch (levelBattle)
        {
            case BattleType.LevelOneBattle:
                OnLevelOneBattleWon?.Invoke();
                break;
            case BattleType.LevelOneBoss:
                OnLevelOneBossWon?.Invoke();
                break;
            case BattleType.LevelTwoBattle:
                OnLevelTwoBattleWon?.Invoke();
                break;
            case BattleType.LevelTwoBoss:
                OnLevelTwoBossWon?.Invoke();
                break;
            case BattleType.LevelThreeBattle:
                OnLevelThreeBattleWon?.Invoke();
                break;
            case BattleType.LevelThreeBoss:
                OnLevelThreeBossWon?.Invoke();
                break;
            case BattleType.Lost:
                OnBattleLost?.Invoke();
                break;
        }
    }
}
