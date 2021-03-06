﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {
    public SoundClips clips;

    private AudioSource _audio;
    private float _volume = 0.5f;

    public override void Awake() {
        base.Awake();
        _audio = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip) {
        _audio.Stop();
        _audio.PlayOneShot(clip, _volume);
    }

    public void IncreaseVolume() {
        _volume = Mathf.Clamp01(_volume + 0.1f);
    }

    public void DecreaseVolume() {
        _volume = Mathf.Clamp01(_volume - 0.1f);
    }

    public float GetVolume() {
        return _volume;
    }
}
