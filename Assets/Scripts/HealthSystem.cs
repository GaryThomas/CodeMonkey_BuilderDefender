using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    public event EventHandler OnDamaged;
    public event EventHandler OnDeath;

    private int _maxHealth = 1;
    private int _health = 1;

    public void SetMaxHealth(int maxHealth, bool setHealth = true) {
        _maxHealth = maxHealth;
        if (setHealth) {
            _health = _maxHealth;
        }
    }

    public void TakeDamage(int amount) {
        _health -= amount;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        OnDamaged?.Invoke(this, EventArgs.Empty);
        if (IsDead()) {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsFullHealth() {
        Debug.Log("Health: " + _health + ", Max: " + _maxHealth);
        return _health >= _maxHealth;
    }

    public bool IsDead() {
        return _health <= 0;
    }

    public int GetHealth() {
        return _health;
    }

    public float GetHealthNormalized() {
        return (float)_health / _maxHealth;
    }
}
