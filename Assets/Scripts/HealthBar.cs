using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] private HealthSystem _health;

    [SerializeField] private Transform _bar;
    [SerializeField] private Transform _separators;
    [SerializeField] private Transform _separator;
    [SerializeField] private Transform _lastSeparator;
    [SerializeField] private int _healthPerSegment = 10;

    private void Start() {
        _separator.gameObject.SetActive(false);
        _lastSeparator.gameObject.SetActive(false);
        BuildSeparators();
        _health.OnDamaged += OnDamaged;
        _health.OnHealed += OnHealed;
        _health.OnHealthMaxChanged += OnHealthMaxChanged;
        UpdateBar();
    }

    private void BuildSeparators() {
        int numSeparators = Mathf.FloorToInt(_health.GetMaxHealth() / _healthPerSegment);
        float segmentSize = (_lastSeparator.localPosition.x - _separator.localPosition.x) / _health.GetMaxHealth();
        // Remove any existing/old separators (all children of _separators)
        foreach (Transform separator in _separators) {
            if (separator != _separator) {
                Destroy(separator.gameObject);
            }
        }
        for (int i = 1; i < numSeparators; i++) {
            Transform xform = Instantiate(_separator, _separators);
            Vector3 pos = _separator.position;
            pos.x += segmentSize * i * _healthPerSegment;
            xform.position = pos;
            xform.gameObject.SetActive(true);
        }
    }

    private void OnHealthMaxChanged(object sender, EventArgs e) {
        BuildSeparators();
    }

    private void OnHealed(object sender, EventArgs e) {
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
