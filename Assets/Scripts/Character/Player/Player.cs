using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter 
{
    public string Name { get; set; }
    public int MaxHealth { get; set; } = 100;
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsDefending { get; set; }

    public List<Ability> Abilities { get; set; } = new();
    
    private LaserBlast _laserBlast;
    private EnergyShield _energyShield;
    private Hack _hack;
    private Troubleshoot _troubleshoot;
    private ElectroPulse _electroPulse;
    private NanoSwarm _nanoSwarm;
    private Incognito _incognito;
    private Mydoom _mydoom;

    [SerializeField] private GameObject _healthText;
    
    private void InitializeAbilities()
    {
        _laserBlast = new LaserBlast();
        _energyShield = new EnergyShield();
        _hack = new Hack();
        _troubleshoot = new Troubleshoot();
        _electroPulse = new ElectroPulse();
        _nanoSwarm = new NanoSwarm();
        _incognito = new Incognito();
        _mydoom = new Mydoom();
    }
    
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
            case (1):
                _electroPulse.IsUnlocked = true;
                break;
            case (2):
                _nanoSwarm.IsUnlocked = true;
                break;
            case (3):
                _incognito.IsUnlocked = true;
                break;
            case (4):
                _mydoom.IsUnlocked = true;
                break;
        }
    }

    private void Awake()
    {
        InitializeAbilities();
        AddAbilities();
        CurrentHealth = MaxHealth;
    }
    
    public void TakeDamage(int amount)
    {
        if (IsDefending)
        {
            amount /= 2;
            IsDefending = false;
        }
        
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        UpdateHealthText(_healthText);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack(ICharacter target, int damageAmount)
    {
        target.TakeDamage(damageAmount);
    }

    public void Defend()
    {
        IsDefending = true;
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
    }

    public void Die()
    {
        Debug.Log("player died");
        IsDead = true;
    }

    public void UpdateHealthText(GameObject textObject)
    {
        textObject.GetComponent<TMP_Text>().text = $"{Name} Health: {CurrentHealth}";
    }
    
}
