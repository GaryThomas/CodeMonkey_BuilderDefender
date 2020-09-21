using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : Singleton<GameOverUI> {
    [SerializeField] Button retryButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] TextMeshProUGUI survivedText;

    public override void Awake() {
        base.Awake();
        Hide();
        retryButton.onClick.AddListener(() => {
            GameSceneManager.LoadScene(GameSceneManager.Scene.Game);
        });
        mainMenuButton.onClick.AddListener(() => {
            GameSceneManager.LoadScene(GameSceneManager.Scene.MainMenu);
        });
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        survivedText.SetText("You made it through " + EnemyWaveManager.Instance.GetWaveNumber() + " waves");
        gameObject.SetActive(true);
    }
}
