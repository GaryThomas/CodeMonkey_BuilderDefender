using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour {

    public event EventHandler OnWaveNumberChanged;

    [SerializeField] private float timeToNextWave;
    [SerializeField] private float timeToSpawnEnemy;
    [SerializeField] private int startingWaveSize = 5;
    [SerializeField] private int waveMultiplier = 3;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform nextSpawnPoint;

    private enum State {
        WaitingToSpawnWave,
        SpawningWave,
    }
    private State _state;
    private float _waveSpawnTimer;
    private float _enemySpawnTimer;
    private int _enemiesToSpawn;
    private Vector3 _spawnPoint;
    private int _waveNumber;

    private void Start() {
        _state = State.WaitingToSpawnWave;
        _waveSpawnTimer = 5f;
        NextSpawnPosition();
    }

    private void Update() {
        switch (_state) {
            case State.WaitingToSpawnWave:
                _waveSpawnTimer -= Time.deltaTime;
                if (_waveSpawnTimer < 0) {
                    SpawnWave();
                }
                break;
            case State.SpawningWave:
                if (_enemiesToSpawn > 0) {
                    _enemySpawnTimer -= Time.deltaTime;
                    if (_enemySpawnTimer < 0) {
                        _enemySpawnTimer = UnityEngine.Random.Range(0, timeToSpawnEnemy);
                        _enemiesToSpawn--;
                        Enemy.CreateEnemy(_spawnPoint + Utils.GetRandomDir() * UnityEngine.Random.Range(0, 10f));
                    }
                } else {
                    _waveSpawnTimer = timeToNextWave;
                    _state = State.WaitingToSpawnWave;
                    NextSpawnPosition();
                }
                break;
        }
    }

    private void SpawnWave() {
        _enemiesToSpawn = startingWaveSize + _waveNumber * waveMultiplier;
        _enemySpawnTimer = timeToSpawnEnemy;
        _state = State.SpawningWave;
        _waveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    private void NextSpawnPosition() {
        _spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position;
        nextSpawnPoint.position = _spawnPoint;
    }

    public int GetWaveNumber() {
        return _waveNumber;
    }

    public float GetTimeToNextWave() {
        return _waveSpawnTimer;
    }
}
