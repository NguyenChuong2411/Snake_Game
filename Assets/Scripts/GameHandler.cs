using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;
using Custom.Utils;

public class GameHandler : MonoBehaviour {

    private static GameHandler instance;

    private void Awake() {
        instance = this;
        Time.timeScale = 1f;
    }

    private void Start() {
        Debug.Log("GameHandler.Start");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsGamePaused()) {
                GameHandler.ResumeGame();
            } else {
                GameHandler.PauseGame();
            }
        }
    }
}
