using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Starter;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private GameObject _hud;
    [SerializeField] private GameObject _battlePanel;
    [SerializeField] private GameObject _dialogPanel;

    private LaserShooter _ls;
    private FirstPersonController _fpc;

    private void Awake()
    {
        _ls = GetComponent<LaserShooter>();
        _fpc = GetComponent<FirstPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BattleZone"))
        {
            _hud.SetActive(false);
            _battlePanel.SetActive(true);
            _ls.enabled = false;
            _fpc.enabled = false;
        }

        // if (other.gameObject.CompareTag("DialogZone"))
        // {
        //     _hud.SetActive(false);
        //     _dialogPanel.SetActive(true);
        //     _ls.enabled = false;
        //     _fpc.enabled = false;
        // }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("BattleZone"))
        {
            _hud.SetActive(false);
            _battlePanel.SetActive(true);
            _ls.enabled = false;
            _fpc.enabled = false;
        }
    }
}
