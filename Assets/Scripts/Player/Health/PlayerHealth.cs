using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void TakeDamage(float damage)
    {
        if (_isInvulnerable)
            return;
        _health -= damage;
        Debug.Log("Player damaged for " + damage + " health remaining: " + _health);
        if (_health <= 0)
        {
            _isDead = true;
            Die();
        }
    }

    public override void Heal(float heal)
    {
        Debug.Log("Player healed for " + heal);
        _health += heal;
        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    protected override void Die()
    {
        Debug.Log("Player died");
        Destroy(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    protected override void Start()
    {
        base.Start();
    }
}
