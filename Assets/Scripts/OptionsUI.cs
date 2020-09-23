using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour {
    [SerializeField] private Button soundIncreaseButton;
    [SerializeField] private Button soundDecreaseButton;
    [SerializeField] private TextMeshProUGUI soundVolumeText;
    [SerializeField] private Button musicIncreaseButton;
    [SerializeField] private Button musicDecreaseButton;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Toggle edgeScrollingToggle;

    private void Awake() {
        soundIncreaseButton.onClick.AddListener(() => {
            SoundManager.Instance.IncreaseVolume();
            ShowSoundVolume();
        });
        soundDecreaseButton.onClick.AddListener(() => {
            SoundManager.Instance.DecreaseVolume();
            ShowSoundVolume();
        });
        musicIncreaseButton.onClick.AddListener(() => {
            MusicManager.Instance.IncreaseVolume();
            ShowMusicVolume();
        });
        musicDecreaseButton.onClick.AddListener(() => {
            MusicManager.Instance.DecreaseVolume();
            ShowMusicVolume();
        });
        mainMenuButton.onClick.AddListener(() => {
            PauseGame(false);
            GameSceneManager.LoadScene(GameSceneManager.Scene.Game);
        });
        edgeScrollingToggle.onValueChanged.AddListener((bool set) => {
            CameraHandler.Instance.SetEdgeScrolling(set);
        });
    }

    private void Start() {
        edgeScrollingToggle.SetIsOnWithoutNotify(CameraHandler.Instance.GetEdgeScrolling());
        ShowMusicVolume();
        ShowSoundVolume();
        gameObject.SetActive(false);
    }

    private void ShowSoundVolume() {
        soundVolumeText.SetText(Mathf.RoundToInt(SoundManager.Instance.GetVolume() * 10f).ToString());
    }

    private void ShowMusicVolume() {
        musicVolumeText.SetText(Mathf.RoundToInt(MusicManager.Instance.GetVolume() * 10f).ToString());
    }

    public void ToggleOptionsUI() {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf) {
            PauseGame(true);
        } else {
            PauseGame(false);
        }
    }

    private void PauseGame(bool state) {
        Time.timeScale = state ? 0f : 1f;
    }
}
