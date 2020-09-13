using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lookForTargetTime = 0.2f;
    [SerializeField] private float targetSearchRadius = 30f;

    private Rigidbody2D _rb2d;
    private Transform _target;
    private float _lookForTargetTimer;

    public static Enemy CreateEnemy(Vector3 position) {
        // Can't think of a way to cache this (yet)
        Transform prefab = Resources.Load<Transform>("Enemy");
        Transform xform = Instantiate(prefab, position, Quaternion.identity);
        return xform.GetComponent<Enemy>();
    }

    private void Start() {
        TargetHQ();
        _rb2d = GetComponent<Rigidbody2D>();
        _lookForTargetTimer = Random.Range(0, lookForTargetTime);
    }

    private void TargetHQ() {
        _target = BuildingManager.Instance.GetHQ();
    }

    private void Update() {
        if (_target == null) {
            // Old target was destroyed, find a new one
            LookForClosestTarget();
            if (_target == null) {
                // Nothing nearby - go after HQ
                TargetHQ();
            }
        }
        Vector3 dir = _target.position - transform.position;
        _rb2d.velocity = dir * moveSpeed;
        _lookForTargetTimer -= Time.deltaTime;
        if (_lookForTargetTimer <= 0) {
            _lookForTargetTimer += lookForTargetTime;
            LookForClosestTarget();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Building building = other.gameObject.GetComponent<Building>();
        if (building != null) {
            HealthSystem health = building.GetComponent<HealthSystem>();
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void LookForClosestTarget() {
        if (_target == null) {
            TargetHQ();
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
