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
    }
}
