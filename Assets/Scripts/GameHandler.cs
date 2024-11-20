using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;
using Custom.Utils;

public class GameHandler : MonoBehaviour {

    private static GameHandler instance;
    private static bool isPaused = false;

    [SerializeField] private Snake snake;

    private LevelGrid levelGrid;

    private void Awake() {
        instance = this;
        Score.InitializeStatic();
        Time.timeScale = 1f;
    }

    private void Start() {
        Debug.Log("GameHandler.Start");

        levelGrid = new LevelGrid(20, 20);

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (!IsGamePaused() && !snake.IsGameOver)
        {
            levelGrid.Update(Time.deltaTime); // Update food timers in LevelGrid
        }
    }
    public static void SnakeDied() {
        bool isNewHighscore = Score.TrySetNewHighscore();
        GameOverWindow.ShowStatic(isNewHighscore);
        ScoreWindow.HideStatic();
    }

    public static void ResumeGame() {
        isPaused = false;
        PauseWindow.HideStatic();
        Time.timeScale = 1f;
    }

    public static void PauseGame() {
        isPaused = true;
        PauseWindow.ShowStatic();
        Time.timeScale = 0f;
    }

    public static bool IsGamePaused() {
        return Time.timeScale == 0f;
    }
}
