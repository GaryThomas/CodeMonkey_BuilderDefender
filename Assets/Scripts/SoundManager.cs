using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {
    public SoundClips clips;

    private AudioSource _audio;

    public override void Awake() {
        base.Awake();
        _audio = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip) {
        _audio.Stop();
        _audio.PlayOneShot(clip);
    }
}
