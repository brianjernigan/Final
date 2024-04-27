using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _player.GetComponent<PlayerAbilities>().HasPhotonBlast)
        {
            Debug.Log("battle!");
        }
        else if (other.gameObject.CompareTag("Player") && !_player.GetComponent<PlayerAbilities>().HasPhotonBlast)
        {
            Debug.Log("does not have photon blast");
        }
    }
}
