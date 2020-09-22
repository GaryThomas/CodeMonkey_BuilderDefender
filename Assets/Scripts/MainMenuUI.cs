using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    private void Awake() {
        // Note the different [crude?] way to get at the elements
        transform.Find("PlayButton").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.LoadScene(GameSceneManager.Scene.Game);
        });
        transform.Find("QuitButton").GetComponent<Button>().onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
