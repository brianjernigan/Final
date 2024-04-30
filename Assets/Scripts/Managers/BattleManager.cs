using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private GameObject _battlePanel;
    [SerializeField] private GameObject[] _abilityButtons;
    [SerializeField] private TMP_Text _updateText;
    
    [Header("Player")]
    [SerializeField] private GameObject _player;
    private PlayerController _pc;
    private FirstPersonController _fpc;
    private List<PlayerAbility> _playerAbilities;

    private void Awake()
    {
        _pc = _player.GetComponent<PlayerController>();
        _fpc = _player.GetComponent<FirstPersonController>();
        _playerAbilities = _pc.Abilities;
    }

    private void PopulateButtons()
    {
        for (int i = 0; i < _abilityButtons.Length; i++)
        {
            _abilityButtons[i].SetActive(_playerAbilities[i].IsUnlocked);
        }
    }

    private void Start()
    {
        PopulateButtons();
    }
}
