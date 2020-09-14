using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour {
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxLifetime = 2f;
    [SerializeField] private int damage = 5;

    private Enemy _target;
    private Vector3 _lastDir;

    public static ArrowProjectile CreateArrowProjectile(Vector3 position, Enemy enemy) {
        // Can't think of a way to cache this (yet)
        Transform prefab = Resources.Load<Transform>("ArrowProjectile");
        Transform xform = Instantiate(prefab, position, Quaternion.identity);
        ArrowProjectile arrowProjectile = xform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(enemy);
        Destroy(arrowProjectile.gameObject, arrowProjectile.maxLifetime);
        return arrowProjectile;
    }

    private void Update() {
        Vector3 dir;
        if (_target != null) {
            dir = (_target.transform.position - transform.position).normalized;
            _lastDir = dir;
        } else {
            dir = _lastDir;
        }
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, Utils.VectorAngle(dir));
    }

    public void SetTarget(Enemy target) {
        _target = target;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null) {
            HealthSystem _enemyHealth = enemy.GetComponent<HealthSystem>();
            _enemyHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
