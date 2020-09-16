using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyWaveUI : MonoBehaviour {
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI waveTimerText;
    [SerializeField] private RectTransform spawnPositionIndicator;

    private Camera _cam;
    private bool _newWave = true;

    private void Start() {
        _cam = Camera.main;
        enemyWaveManager.OnWaveNumberChanged += WaveNumberChanged;
    }

    private void SetWaveNumberText(string str) {
        waveNumberText.SetText(str);
    }

    private void SetWaveTimerText(string str) {
        waveTimerText.SetText(str);
    }

    private void WaveNumberChanged(object sender, EventArgs e) {
        SetWaveNumberText("Wave #" + enemyWaveManager.GetWaveNumber());
    }

    private void Update() {
        float timeToNextWave = enemyWaveManager.GetTimeToNextWave();
        if (timeToNextWave < 0f) {
            SetWaveTimerText("");
            _newWave = true;
        } else {
            if (_newWave) {
                _newWave = false;
                Vector3 enemyDir = (enemyWaveManager.GetNextSpawnPoint() - _cam.transform.position).normalized;
                spawnPositionIndicator.eulerAngles = new Vector3(0, 0, Utils.VectorAngle(enemyDir));
                spawnPositionIndicator.anchoredPosition = enemyDir * 300f;
            }
            SetWaveTimerText("Next wave in " + timeToNextWave.ToString("F1") + " secs");
        }
    }
}
