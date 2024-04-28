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
    
    private BattleState _currentBattleState;

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

    private void PopulateAbilityButtons(GameObject target)
    {
        for (var i = 0; i < _abilityButtons.Length; i++)
        {
            _abilityButtons[i].SetActive(_pa.Abilities[i].IsUnlocked);
            var buttonIndex = i;
            _abilityButtons[i].GetComponent<Button>().onClick.AddListener(() => _pa.Abilities[buttonIndex].Activate(target));
        }
    }

    public void StartBattle(GameObject target)
    {
        EnterBattleMode();
        PopulateAbilityButtons(target);
        _currentBattleState = BattleState.PlayerTurn;
        StartCoroutine(BattleRoutine());
    }

    private IEnumerator BattleRoutine()
    {
        while (true)
        {
            if (_currentBattleState == BattleState.PlayerTurn)
            {
                Debug.Log("Player's Turn");
                PlayerHasTakenTurn = false;
                ActivateButtons();
                yield return new WaitUntil(() => PlayerHasTakenTurn);
                _currentBattleState = BattleState.EnemyTurn;
            } else if (_currentBattleState == BattleState.EnemyTurn)
            {
                Debug.Log("Enemy's turn");
                EnemyHasTakenTurn = false;
                DeactivateButtons();
                _enemy.GetComponent<Enemy>().TakeTurn();
                yield return new WaitUntil(() => EnemyHasTakenTurn);
                _currentBattleState = BattleState.PlayerTurn;
            }
        }

        yield return null;
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
