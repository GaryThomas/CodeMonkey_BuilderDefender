using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField] private float lookForTargetTime = 0.2f;
    [SerializeField] private float shootTargetTime = 0.5f;
    [SerializeField] private float targetSearchRadius = 20f;
    [SerializeField] private Transform spawnPoint;

    private Enemy _target;
    private float _lookForTargetTimer;
    private float _shootTargetTimer;

    private void Start() {
        _lookForTargetTimer = Random.Range(0, lookForTargetTime);
        _shootTargetTimer = Random.Range(0, shootTargetTime);
    }

    private void Update() {
        HandleTargeting();
        HandleShooting();
    }

    private void HandleTargeting() {
        _lookForTargetTimer -= Time.deltaTime;
        if (_lookForTargetTimer <= 0) {
            _lookForTargetTimer += lookForTargetTime;
            LookForClosestTarget();
        }
    }

    private void HandleShooting() {
        if (_target != null) {
            _shootTargetTimer -= Time.deltaTime;
            if (_shootTargetTimer <= 0) {
                _shootTargetTimer += shootTargetTime;
                ArrowProjectile.CreateArrowProjectile(spawnPoint.position, _target);
            }
        }
    }

    private void LookForClosestTarget() {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, targetSearchRadius);
        foreach (Collider2D target in targets) {
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null) {
                if (_target == null ||
                    (Vector3.Distance(transform.position, enemy.transform.position) <
                    Vector3.Distance(transform.position, _target.transform.position))
                ) {
                    _target = enemy;
                }
            }
        }
    }
}
