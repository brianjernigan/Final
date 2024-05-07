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
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinController : MonoBehaviour
{
    private const float RotationSpeed = 45.0f;

    private void Update()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ChangeState(GameState.NextLevel);
        }

        if (gameObject.name == "Coin2" && other.CompareTag("Player"))
        {
            GameManager.Instance.ChangeState(GameState.Won);
        }
    }
}
