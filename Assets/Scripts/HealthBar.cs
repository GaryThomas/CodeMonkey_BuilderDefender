using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] HealthSystem _health;

    private Transform _bar;

    private void Start() {
        _bar = transform.Find("bar");
        _health.OnDamaged += OnDamaged;
        UpdateBar();
    }

    private void OnDamaged(object sender, EventArgs e) {
        UpdateBar();
    }

    private void UpdateBar() {
        if (_health.IsFullHealth()) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
            Vector3 scale = _bar.localScale;
            scale.x = _health.GetHealthNormalized();
            _bar.localScale = scale;
        }
    }
}
