using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemyHealthText;
    
    public int MaxHealth { get; private set; } = 100;
    public int CurrentHealth { get; set; }
    private bool _isDefending;

    [SerializeField] private Material _damageMat;
    private Material _originalMaterial;
    private MeshRenderer _mr;

    [SerializeField] private GameObject _player;
    
    protected virtual void Awake()
    {
        CurrentHealth = MaxHealth;
        _mr = GetComponent<MeshRenderer>();
        _originalMaterial = _mr.material;
        UpdateHealthText();
    }

    public virtual void TakeDamage(int amount)
    {
        if (_isDefending)
        {
            amount /= 2;
            _isDefending = false;
        }
        
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        UpdateHealthText();
        StartCoroutine(FlashDamageColor());

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected void Attack(GameObject target)
    {
        target.GetComponent<PlayerHealth>().TakeDamage(10);
        BattleManager.Instance.EnemyHasTakenTurn = true;
    }

    protected void Defend()
    {
        _isDefending = true;
        BattleManager.Instance.EnemyHasTakenTurn = true;
    }

    protected void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        BattleManager.Instance.EnemyHasTakenTurn = true;
    }

    public void TakeTurn()
    {
        var randomNumber = Random.Range(1, 4);

        switch (randomNumber)
        {
            case (1):
                Attack(_player);
                UpdateHealthText();
                break;
            case (2):
                Defend();
                UpdateHealthText();
                break;
            case (3):
                Heal(5);
                UpdateHealthText();
                break;
        }
    }

    protected virtual void Die()
    {
        Debug.Log("enemy died");
    }

    protected IEnumerator FlashDamageColor()
    {
        _mr.material = _damageMat;
        yield return new WaitForSeconds(0.1f);
        _mr.material = _originalMaterial;
    }

    protected void UpdateHealthText()
    {
        _enemyHealthText.text = $"Enemy Health: {CurrentHealth}";
    }
}
