using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _hud;
    
    private LaserShooter _ls;
    private FirstPersonController _fpc;

    private void Awake()
    {
        _ls = _player.GetComponent<LaserShooter>();
        _fpc = _player.GetComponent<FirstPersonController>();
    }

    public void OnClickBackButton()
    {
        gameObject.SetActive(false);
        _hud.SetActive(true);
        _ls.enabled = true;
        _fpc.enabled = true;
    }
}
