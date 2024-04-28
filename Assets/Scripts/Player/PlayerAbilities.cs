using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public List<Ability> Abilities { get; set; } = new();

    private LaserBlast _laserBlast;
    private EnergyShield _energyShield;
    private Hack _hack;
    private Troubleshoot _troubleshoot;
    private ElectroPulse _electroPulse;
    private NanoSwarm _nanoSwarm;
    private Incognito _incognito;
    private Mydoom _mydoom;

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

    private void OnEnable()
    {
        DialogManager.Instance.OnLevelOneDialogFinished += UnlockElectroPulse;
    }

    private void OnDisable()
    {
        DialogManager.Instance.OnLevelOneDialogFinished -= UnlockElectroPulse;
    }

    private void Awake()
    {
        InitializeAbilities();
        AddAbilities();
    }

    private void UnlockElectroPulse()
    {
        _electroPulse.IsUnlocked = true;
    }

    private void UnlockNanoSwarm()
    {
        _nanoSwarm.IsUnlocked = true;
    }

    private void UnlockIncognito()
    {
        _incognito.IsUnlocked = true;
    }

    private void UnlockMydoom()
    {
        _mydoom.IsUnlocked = true;
    }
}
