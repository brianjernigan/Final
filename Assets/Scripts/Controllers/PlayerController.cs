using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum AbilityType
{
    LaserBlast,
    EnergyShield,
    Hack,
    Troubleshoot,
    ElectroPulse,
    NanoSwarm,
    Incognito,
    Mydoom
}

public class PlayerController : MonoBehaviour, ICharacter 
{
    public static PlayerController Instance { get; private set; }

    public string Name { get; set; } = "Player";
    public int MaxHealth { get; set; } = 100;
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsDefending { get; set; }
    public bool IsStunned { get; set; }
    public bool IsConfused { get; set; }
    public bool IsSwarmed { get; set; }
    public bool IsHiding { get; set; }
    public bool IsCharging { get; set; }

    public List<PlayerAbility> Abilities { get; } = new();

    private readonly LaserBlast _laserBlast = new();
    private readonly EnergyShield _energyShield = new();
    private readonly Hack _hack = new();
    private readonly Troubleshoot _troubleshoot = new();
    private readonly ElectroPulse _electroPulse = new();
    private readonly NanoSwarm _nanoSwarm = new();
    private readonly Incognito _incognito = new();
    private readonly Mydoom _mydoom = new();

    [SerializeField] private GameObject _healthText;
    
    private void AddAbilities()
    {
        Abilities.Add(_laserBlast);
        Abilities.Add(_energyShield);
        Abilities.Add(_hack);
        Abilities.Add(_troubleshoot);
        Abilities.Add(_electroPulse);
        Abilities.Add(_nanoSwarm);
        Abilities.Add(_incognito);
        Abilities.Add(_mydoom);
    }

    public void UnlockAbility(int abilityNumber)
    {
        switch (abilityNumber)
        {
            case 0:
                _electroPulse.IsUnlocked = true;
                break;
            case 1:
                _nanoSwarm.IsUnlocked = true;
                break;
            case 2:
                _incognito.IsUnlocked = true;
                break;
            case 3:
                _mydoom.IsUnlocked = true;
                break;
        }
    }

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
        AddAbilities();
        CurrentHealth = MaxHealth;
    }
    
    #region ICharacterImplementation

    public void TakeDamage(int amount)
    {
        if (IsDefending)
        {
            amount /= 2;
            IsDefending = false;
        }
        
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);

        if (CurrentHealth <= 0)
        {
            IsDead = true;
        }
    }

    #endregion
}
