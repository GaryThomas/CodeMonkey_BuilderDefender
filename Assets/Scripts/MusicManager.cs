using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager> {
    private AudioSource _audio;
    private float _volume = 0.5f;

    public override void Awake() {
        base.Awake();
        _audio = GetComponent<AudioSource>();
        UpdateVolume();
    }

    public void IncreaseVolume() {
        _volume = Mathf.Clamp01(_volume + 0.1f);
        UpdateVolume();
    }

    public void DecreaseVolume() {
        _volume = Mathf.Clamp01(_volume - 0.1f);
        UpdateVolume();
    }

    public float GetVolume() {
        return _volume;
    }

    private void UpdateVolume() {
        _audio.volume = _volume;
    }
}
