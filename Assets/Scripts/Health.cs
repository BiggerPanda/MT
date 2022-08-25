using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float _maxHealth;
    protected float _health; 
    protected bool _isDead;
    protected bool _isInvulnerable;

    public event System.Action OnDeath;
    public event System.Action OnHit;
    public event System.Action OnHeal;

    public abstract void TakeDamage(float damage);
    public abstract void Heal(float heal);
    protected abstract void Die();

    protected virtual void Start()
    {
        _health = _maxHealth;
    }
}
