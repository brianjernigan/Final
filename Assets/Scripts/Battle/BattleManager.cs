using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
    PlayerTurn,
    EnemyTurn,
    Win,
    Loss
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
    private PlayerAbilities _pa;
    private PlayerHealth _ph;

    [Header("Enemy")] 
    [SerializeField] private GameObject _enemy;
    private Enemy _enemyScript;
    
    private BattleState _currentBattleState;
    public string PlayerMove { get; set; }
    public string EnemyMove { get; set; }

    public bool PlayerHasTakenTurn { get; set; }
    public bool EnemyHasTakenTurn { get; set; }
    
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
        _pa = _player.GetComponent<PlayerAbilities>();
        _ph = _player.GetComponent<PlayerHealth>();
        _enemyScript = _enemy.GetComponent<Enemy>();
        _currentBattleState = BattleState.PlayerTurn;
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
        for (var i = 0; i < _abilityButtons.Length; i++)
        {
            InitializeButtons(i);
        }
    }

    private void InitializeButtons(int buttonIndex)
    {
        _abilityButtons[buttonIndex].SetActive(_pa.Abilities[buttonIndex].IsUnlocked);
        _abilityButtons[buttonIndex].GetComponent<Button>().onClick.AddListener(() => _pa.Abilities[buttonIndex].Activate(_player, _enemy));
        _abilityButtons[buttonIndex].GetComponent<Button>().onClick.AddListener(() => UpdateButtonText(_abilityButtons[buttonIndex], _pa.Abilities[buttonIndex]));
        _abilityButtons[buttonIndex].GetComponentInChildren<TMP_Text>().text = _pa.Abilities[buttonIndex].ToString();
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
        while (!_ph.IsDead && !_enemyScript.IsDead)
        {
            if (_currentBattleState == BattleState.PlayerTurn)
            {
                Debug.Log("Player's Turn");
                PlayerHasTakenTurn = false;
                ActivateButtons();
                yield return new WaitUntil(() => PlayerHasTakenTurn);
                UpdateActionText(_player, PlayerMove);
                _currentBattleState = BattleState.EnemyTurn;
            } else if (_currentBattleState == BattleState.EnemyTurn)
            {
                Debug.Log("Enemy's turn");
                EnemyHasTakenTurn = false;
                DeactivateButtons();
                yield return new WaitForSeconds(2.5f);
                _enemy.GetComponent<Enemy>().TakeTurn();
                yield return new WaitUntil(() => EnemyHasTakenTurn);
                UpdateActionText(_enemy, EnemyMove);
                _currentBattleState = BattleState.PlayerTurn;
            }
        }

        if (_ph.IsDead)
        {
            Debug.Log("Player loses");
            _currentBattleState = BattleState.Win;
            ExitBattleMode();
        } else if (_enemyScript.IsDead)
        {
            Debug.Log("Player win");
            _currentBattleState = BattleState.Loss;
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

    private void PlayerTurn()
    {
        throw new NotImplementedException();
    }

    private void EnemyTurn()
    {
        throw new NotImplementedException();
    }
}
