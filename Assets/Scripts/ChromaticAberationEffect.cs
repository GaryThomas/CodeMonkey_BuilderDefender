using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChromaticAberationEffect : Singleton<ChromaticAberationEffect> {
    [SerializeField] private float _fadeSpeed = 1f;

    private Volume _volume;

    public override void Awake() {
        base.Awake();
        _volume = GetComponent<Volume>();
    }

    private void Update() {
        if (_volume.weight > 0) {
            _volume.weight -= Time.deltaTime * _fadeSpeed;
        }
    }

    public void SetWeight(float weight) {
        _volume.weight = Mathf.Clamp01(weight);
    }
}
