using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject gameStartPanel;

    public GameObject pausePanel;
    public GameObject pauseButton;
    public bool isGameInProgress = false;
    public bool isGamePause = false;

    public GameObject gameOverPanel;
    public bool isGameOver = false;

    public bool isBlackHoleSpawn;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 0;
        isGameInProgress = false;
        gameStartPanel.SetActive(true);
    }

    public void GameStart()
    {
        Time.timeScale = 1;
        gameStartPanel.SetActive(false);
        isGameInProgress = true;
        SoundManager.instance.PlayGameBGM(isGameInProgress);
    }

    public void PauseControl()
    {
        isGamePause = !isGamePause;
        isGameInProgress = !isGameInProgress;
        Time.timeScale = isGamePause ? 0 : 1;

        pausePanel.SetActive(isGamePause);
        pauseButton.SetActive(isGameInProgress);

        SoundManager.instance.PlayGameBGM(isGameInProgress);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(isGameOver);
        Time.timeScale = 0;
    }
}
