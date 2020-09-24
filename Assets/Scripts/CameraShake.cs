using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : Singleton<CameraShake> {

    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _noise;
    private float _timer;
    private float _timerMax;
    private float _intensity;

    public override void Awake() {
        base.Awake();
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update() {
        if (_timer < _timerMax) {
            _timer += Time.deltaTime;
            _noise.m_AmplitudeGain = Mathf.Lerp(_intensity, 0, _timer / _timerMax);
        }
    }

    public void Shake(float intensity, float timerMax) {
        _timer = 0;
        _timerMax = timerMax;
        _intensity = intensity;
        _noise.m_AmplitudeGain = _intensity;
    }
}
