using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Enemy
{
    public string name;
    public int maxHealth;
    public Attack attack;
    public Defend defend;
    public Heal heal;

    public Enemy(string enemyName, int enemyHealth, int attackPower, int healPower)
    {
        name = enemyName;
        maxHealth = enemyHealth;
        attack = new Attack(attackPower);
        heal = new Heal(healPower);
        defend = new Defend();
    }
}

public class EnemyController : MonoBehaviour, ICharacter
{
    [SerializeField] private Material _damageMat;
    private Material _originalMat;
    private MeshRenderer _meshRenderer;

    private GameObject _player;
    private PlayerController _playerController;
    
    private ICharacter _playerEntity;
    private ICharacter _enemyEntity;

    public string Name { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsDefending { get; set; }
    public bool IsStunned { get; set; }
    public bool IsConfused { get; set; }
    public bool IsSwarmed { get; set; }
    public bool IsHiding { get; set; }
    public bool IsCharging { get; set; }

    public List<Ability> Abilities { get; } = new();

    private bool _isBoss;

    private int LevelIndex => DetermineLevelIndex();

    private Attack _attack;
    private Defend _defend;
    private Heal _heal;
    
    private void Awake()
    {
        InitializeScriptComponents();
        InitializeEnemy();
    }

    private int DetermineLevelIndex()
    {
        if (SceneManager.GetActiveScene().name.Contains("0"))
        {
            return 0;
        }

        if (SceneManager.GetActiveScene().name.Contains("1"))
        {
            return 1;
        }

        if (SceneManager.GetActiveScene().name.Contains("2"))
        {
            return 2;
        }

        throw new ArgumentException("Not a valid level. Enemy not configured.");
    }
    
    private void InitializeScriptComponents()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalMat = _meshRenderer.material;
        _playerController = _player.GetComponent<PlayerController>();
        _playerEntity = _player.GetComponent<ICharacter>();
    }
    
    private void InitializeEnemy()
    {
        _enemyEntity = GetComponent<ICharacter>();
        _isBoss = IsEnemyBoss();

        ConfigureEnemy();
        AddAbilities();
    }
    
    private bool IsEnemyBoss()
    {
        var enemyName = gameObject.name;
        return enemyName.Contains("Boss");
    }

    private void ConfigureEnemy()
    {
        Dictionary<(int, bool), Enemy> enemyConfiguration = new()
        {
            // Standard enemies
            { (0, false), new Enemy("Beeboop",10, 3, 2) },
            { (1, false), new Enemy("B2J2",15, 6, 4) },
            { (2, false), new Enemy("BJ3396", 20,9, 6) },
            // Bosses
            { (0, true), new Enemy("Awesome-O",30, 6, 4) },
            { (1, true), new Enemy("HAL", 35, 12, 8) },
            { (2, true), new Enemy("T-1000",40, 18, 12) }
        };

        if (!enemyConfiguration.TryGetValue((LevelIndex, _isBoss), out var currentEnemy)) return;
        Name = currentEnemy.name;
        MaxHealth = currentEnemy.maxHealth;
        _attack = currentEnemy.attack;
        _heal = currentEnemy.heal;
        _defend = currentEnemy.defend;
        CurrentHealth = MaxHealth;
    }

    private void AddAbilities()
    {
        Abilities.Add(_attack);
        Abilities.Add(_defend);
        Abilities.Add(_heal);
    }
    
    public IEnumerator TakeTurn()
    {
        if (IsSwarmed)
        {
            BattleManager.Instance.UpdateActionText($"{Name} took damage from Nano Swarm!");
            if (_playerController.Abilities.Find(ability => ability.Name == "Nano Swarm") is NanoSwarm nanoSwarm)
            {
                TakeDamage(nanoSwarm.DamagePerTurn);
            }

            yield return new WaitForSeconds(1.25f);
            
            IsSwarmed = false;
        }
        
        var randomNumber = Random.Range(0, Abilities.Count);

        Abilities[randomNumber].Activate(_enemyEntity, _playerEntity);

        BattleManager.Instance.UpdateHealthText(gameObject);
    }
    
    private IEnumerator FlashDamageColor()
    {
        _meshRenderer.material = _damageMat;
        yield return new WaitForSeconds(0.1f);
        _meshRenderer.material = _originalMat;
    }
    
    #region ICharacterImplementation

    public void TakeDamage(int amount)
    {
        if (IsDefending && !IsSwarmed)
        {
            amount /= 2;
            IsDefending = false;
        }
        
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        BattleManager.Instance.UpdateHealthText(gameObject);
        StartCoroutine(FlashDamageColor());

        if (CurrentHealth <= 0)
        {
            IsDead = true;
            BattleManager.Instance.EnemyHasTakenTurn = true;
        }
    }

    public void ApplyStatusEffect()
    {
        throw new NotImplementedException();
    }

    #endregion
}
