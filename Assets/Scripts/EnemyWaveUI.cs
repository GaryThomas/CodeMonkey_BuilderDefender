using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnemyWaveUI : MonoBehaviour {
    [SerializeField] EnemyWaveManager enemyWaveManager;
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI waveTimerText;

    private void Start() {
        enemyWaveManager.OnWaveNumberChanged += WaveNumberChanged;
    }

    private void SetWaveNumberText(string str) {
        waveNumberText.SetText(str);
    }

    private void SetWaveTimerText(string str) {
        waveTimerText.SetText(str);
    }

    private void WaveNumberChanged(object sender, EventArgs e) {
        SetWaveNumberText("Wave #" + (enemyWaveManager.GetWaveNumber() + 1));
    }

    private void Update() {
        float timeToNextWave = enemyWaveManager.GetTimeToNextWave();
        if (timeToNextWave < 0f) {
            SetWaveTimerText("");
        } else {
            SetWaveTimerText("Next wave in " + timeToNextWave.ToString("F1") + " secs");
        }
    }
}
