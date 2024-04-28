using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [Header("Panels")] 
    [SerializeField] private GameObject _battlePanel;

    [SerializeField] private GameObject _player;
    private LaserShooter _ls;
    private FirstPersonController _fpc;
    private PlayerAbilities _pa;

    [SerializeField] private GameObject[] _abilityButtons;

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
        _ls = _player.GetComponent<LaserShooter>();
        _fpc = _player.GetComponent<FirstPersonController>();
        _pa = _player.GetComponent<PlayerAbilities>();
    }

    private void EnterBattleMode()
    {
        _battlePanel.SetActive(true);
        _ls.enabled = false;
        _fpc.enabled = false;
        
        PopulateAbilityButtons();
    }

    private void ExitBattleMode()
    {
        _battlePanel.SetActive(false);
        _ls.enabled = true;
        _fpc.enabled = true;
    }

    private void PopulateAbilityButtons()
    {
        for (var i = 0; i < _abilityButtons.Length; i++)
        {
            if (_pa.Abilities[i].IsUnlocked)
            {
                _abilityButtons[i].SetActive(true);
            }
            else
            {
                _abilityButtons[i].SetActive(false);
            }
        }
    }

    public void StartBattle()
    {
        EnterBattleMode();
    }
}
