using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager {
    public enum Scene {
        Game,
        MainMenu,
    }

    public static void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}