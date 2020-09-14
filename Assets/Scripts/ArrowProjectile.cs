using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour {
    [SerializeField] private float moveSpeed;

    private Enemy _target;

    public static ArrowProjectile CreateArrowProjectile(Vector3 position, Enemy enemy) {
        // Can't think of a way to cache this (yet)
        Transform prefab = Resources.Load<Transform>("ArrowProjectile");
        Transform xform = Instantiate(prefab, position, Quaternion.identity);
        ArrowProjectile arrowProjectile = xform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(enemy);
        return arrowProjectile;
    }

    private void Update() {
        if (_target != null) {
            Vector3 dir = (_target.transform.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, Utils.VectorAngle(dir));
        } else {
            Debug.Log("Arrow target vanished");
            Destroy(gameObject);
        }
    }

    public void SetTarget(Enemy target) {
        _target = target;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null) {
            Destroy(enemy.gameObject);
            Destroy(gameObject);
        }
    }
}
