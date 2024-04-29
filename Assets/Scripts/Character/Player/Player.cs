using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter 
{
    public static Player Instance { get; private set; }
    
    public string Name { get; set; }
    public int MaxHealth { get; set; } = 100;
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsDefending { get; set; }

    public List<PlayerAbility> Abilities { get; set; } = new();

    private LaserBlast _laserBlast = new LaserBlast();
    private EnergyShield _energyShield = new EnergyShield();
    private Hack _hack = new Hack();
    private Troubleshoot _troubleshoot = new Troubleshoot();
    private ElectroPulse _electroPulse = new ElectroPulse();
    private NanoSwarm _nanoSwarm = new NanoSwarm();
    private Incognito _incognito = new Incognito();
    private Mydoom _mydoom = new Mydoom();

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
        transform.position = new Vector3(6, 0, -8);
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
            IsDead = true;
        }
    }

    public void UpdateHealthText(GameObject textObject)
    {
        textObject.GetComponent<TMP_Text>().text = $"{Name} Health: {CurrentHealth}";
    }
    
}
