using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, ICharacter
{
    [SerializeField] private GameObject _healthText;
    
    [SerializeField] private Material _damageMat;
    private Material _originalMat;
    private MeshRenderer _meshRenderer;

    [SerializeField] private GameObject _player;
    
    private ICharacter _playerEntity;
    private ICharacter _enemyEntity;

    public string Name { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsDefending { get; set; }
    public bool IsStunned { get; set; }
    public bool IsConfused { get; set; }

    public List<Ability> Abilities { get; set; } = new();

    private Attack _attack = new();
    private Defend _defend = new();
    private Heal _heal = new();
    
    private void InitializeComponents()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalMat = _meshRenderer.material;
        _playerEntity = _player.GetComponent<ICharacter>();
    }

    private void AddAbilities()
    {
        Abilities.Add(_attack);
        Abilities.Add(_defend);
        Abilities.Add(_heal);
    }
    
    private void Awake()
    {
        InitializeComponents();
        InitializeEnemy();
    }

    private void DetermineName()
    {
        var enemyName = gameObject.name;
        var enemyIsBoss = enemyName.Contains("Boss");
        if (!enemyIsBoss)
        {
            if (SceneManager.GetActiveScene().name.Contains("0"))
            {
                Name = "XYZ900";
            }

            if (SceneManager.GetActiveScene().name.Contains("1"))
            {
                Name = "ABC4000";
            }

            if (SceneManager.GetActiveScene().name.Contains("2"))
            {
                Name = "Jimmy";
            }
        } 
        else 
        {
            if (SceneManager.GetActiveScene().name.Contains("0"))
            {
                Name = "Awesome-O";
            }

            if (SceneManager.GetActiveScene().name.Contains("1"))
            {
                Name = "Skynet";
            }

            if (SceneManager.GetActiveScene().name.Contains("2"))
            {
                Name = "T-1000";
            }
        }
    }

    private void InitializeEnemy()
    {
        _enemyEntity = GetComponent<ICharacter>();
        InitializeHealth();
        DetermineName();
        AddAbilities();
    }

    private void InitializeHealth()
    {
        if (gameObject.name.Contains("Enemy"))
        {
            MaxHealth = 20;
        } else if (gameObject.name.Contains("Boss"))
        {
            MaxHealth = 50;
        }

        CurrentHealth = MaxHealth;
    }
    
    public void TakeTurn()
    {
        var randomNumber = Random.Range(0, Abilities.Count);

        Abilities[randomNumber].Activate(_enemyEntity, _playerEntity);

        UpdateHealthText();
    }
    
    private IEnumerator FlashDamageColor()
    {
        _meshRenderer.material = _damageMat;
        yield return new WaitForSeconds(0.1f);
        _meshRenderer.material = _originalMat;
    }

    public void UpdateHealthText()
    {
        _healthText.GetComponent<TMP_Text>().text = $"{Name} Health: {CurrentHealth}";
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
        UpdateHealthText();
        StartCoroutine(FlashDamageColor());

        if (CurrentHealth <= 0)
        {
            IsDead = true;
        }
    }

    public void ApplyStatusEffect()
    {
        throw new NotImplementedException();
    }

    #endregion
}
