using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SoundClips")]
public class SoundClips : ScriptableObject {
    public AudioClip BuildingDamaged;
    public AudioClip BuildingDestroyed;
    public AudioClip BuildingPlaced;
    public AudioClip EnemyDie;
    public AudioClip EnemyHit;
    public AudioClip EnemyWaveStarting;
    public AudioClip GameOver;
    public AudioClip Music;
}
