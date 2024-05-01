using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    public int MaxHealth { get; set; } = 20;
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
        _enemyEntity = GetComponent<ICharacter>();
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
        AddAbilities();
        CurrentHealth = MaxHealth;
    }
    
    public void TakeTurn()
    {
        var randomNumber = Random.Range(0, Abilities.Count);

        Abilities[randomNumber].Activate(_enemyEntity, _playerEntity);

        UpdateHealthText(_healthText);
    }
    
    private IEnumerator FlashDamageColor()
    {
        _meshRenderer.material = _damageMat;
        yield return new WaitForSeconds(0.1f);
        _meshRenderer.material = _originalMat;
    }

    public void UpdateHealthText(GameObject textObject)
    {
        textObject.GetComponent<TMP_Text>().text = $"{Name} Health: {CurrentHealth}";
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
        UpdateHealthText(_healthText);
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
