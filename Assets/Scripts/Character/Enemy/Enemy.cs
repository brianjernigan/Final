using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter
{
    [SerializeField] private GameObject _healthText;
    
    [SerializeField] private Material _damageMat;
    private Material _originalMat;
    private MeshRenderer _meshRenderer;

    [SerializeField] private GameObject _player;

    public string Name { get; set; }
    public int MaxHealth { get; set; } = 20;
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsDefending { get; set; }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalMat = _meshRenderer.material;
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
        StartCoroutine(FlashDamageColor());

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack(ICharacter target, int damageAmount)
    {
        target.TakeDamage(2);
        BattleManager.Instance.EnemyMoveName = "Attack";
        BattleManager.Instance.EnemyHasTakenTurn = true;
    }

    public void Defend()
    {
        IsDefending = true;
        BattleManager.Instance.EnemyMoveName = "Defend";
        BattleManager.Instance.EnemyHasTakenTurn = true;
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        BattleManager.Instance.EnemyMoveName = $"Heal ({amount})";
        BattleManager.Instance.EnemyHasTakenTurn = true;
    }

    public void Die()
    {
        Debug.Log("enemy died");
        IsDead = true;
    }
    
    public void TakeTurn()
    {
        var randomNumber = Random.Range(1, 4);

        switch (randomNumber)
        {
            case 1:
                Attack(_player.GetComponent<ICharacter>(), 5);
                UpdateHealthText(_healthText);
                break;
            case 2:
                Defend();
                UpdateHealthText(_healthText);
                break;
            case 3:
                Heal(5);
                UpdateHealthText(_healthText);
                break;
        }
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
}
