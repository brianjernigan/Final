using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DialogManager _dialogManager;
    [SerializeField] private BattleManager _battleManager;

    public DialogManager DialogManager => _dialogManager;
    public BattleManager BattleManager => _battleManager;
    
    public static UIManager Instance { get; private set; }

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
}
