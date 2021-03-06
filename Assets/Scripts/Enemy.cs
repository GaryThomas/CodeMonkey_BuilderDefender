﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lookForTargetTime = 0.2f;
    [SerializeField] private float targetSearchRadius = 30f;
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private Transform dieParticles;
    [SerializeField] private float shakeOnDamageAmount = 5f;
    [SerializeField] private float shakeOnDamageDuration = 0.1f;
    [SerializeField] private float shakeOnDeathAmount = 7f;
    [SerializeField] private float shakeOnDeathDuration = 0.15f;
    [SerializeField] private float aberationOnDamageLevel = 0.5f;
    [SerializeField] private float aberationOnDeathLevel = 0.5f;

    private Rigidbody2D _rb2d;
    private Transform _target;
    private float _lookForTargetTimer;
    private HealthSystem _health;

    public static Enemy CreateEnemy(Vector3 position) {
        Transform xform = Instantiate(GameAssets.Instance.enemyPrefab, position, Quaternion.identity);
        Enemy enemy = xform.GetComponent<Enemy>();
        xform.GetComponent<HealthSystem>().SetMaxHealth(enemy.maxHealth);
        return enemy;
    }

    private void Start() {
        TargetHQ();
        _rb2d = GetComponent<Rigidbody2D>();
        _lookForTargetTimer = Random.Range(0, lookForTargetTime);
        _health = GetComponent<HealthSystem>();
        _health.OnDeath += EnemyDied;
        _health.OnDamaged += EnemyDamaged;
    }

    private void EnemyDamaged(object sender, System.EventArgs e) {
        SoundManager.Instance.PlayClip(SoundManager.Instance.clips.EnemyHit);
        CameraShake.Instance.Shake(shakeOnDamageAmount, shakeOnDamageDuration);
        ChromaticAberationEffect.Instance.SetWeight(aberationOnDamageLevel);
    }

    private void EnemyDied(object sender, System.EventArgs e) {
        Debug.Log("Enemy died");
        SoundManager.Instance.PlayClip(SoundManager.Instance.clips.EnemyDie);
        Instantiate(dieParticles, transform.position, Quaternion.identity);
        CameraShake.Instance.Shake(shakeOnDeathAmount, shakeOnDeathDuration);
        ChromaticAberationEffect.Instance.SetWeight(aberationOnDeathLevel);
        Destroy(gameObject);
    }

    private void TargetHQ() {
        if (BuildingManager.Instance != null) {
            _target = BuildingManager.Instance.GetHQ();
        }
    }

    private void Update() {
        HandleMovement();
        HandleTargeting();
    }

    private void HandleTargeting() {
        _lookForTargetTimer -= Time.deltaTime;
        if (_lookForTargetTimer <= 0) {
            _lookForTargetTimer += lookForTargetTime;
            LookForClosestTarget();
        }
    }

    private void HandleMovement() {
        if (_target == null) {
            // Old target was destroyed, find a new one
            LookForClosestTarget();
            if (_target == null) {
                // Nothing nearby - go after HQ
                TargetHQ();
                if (_target == null) {
                    // No more HQ!
                    Destroy(gameObject);
                    return;
                }
            }
        }
        Vector3 dir = (_target.position - transform.position).normalized;
        _rb2d.velocity = dir * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Building building = other.gameObject.GetComponent<Building>();
        if (building != null) {
            HealthSystem health = building.GetComponent<HealthSystem>();
            health.TakeDamage(damage);
            // Debug.Log("Destroy Enemy");
            _health.TakeDamage(_health.GetMaxHealth() + 1);
        }
    }

    private void LookForClosestTarget() {
        if (_target == null) {
            TargetHQ();
            if (_target == null) {
                return;
            }
        }
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, targetSearchRadius);
        foreach (Collider2D target in targets) {
            Building building = target.GetComponent<Building>();
            if (building != null) {
                if (Vector3.Distance(transform.position, building.transform.position) <
                    Vector3.Distance(transform.position, _target.position)
                ) {
                    _target = building.transform;
                }
            }
        }
    }
}
