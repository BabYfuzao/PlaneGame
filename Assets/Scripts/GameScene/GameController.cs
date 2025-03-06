using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("-Player related UI-")]
    public GameObject[] weaponIcon;

    [Header("Panel")]
    public GameObject gameStartPanel;

    public GameObject pausePanel;
    public GameObject pauseButton;

    public GameObject gameOverPanel;

    [Header("-Function-")]
    public bool isBlackHoleSpawn;

    [Header("-Game Status-")]
    public int level;
    public int timerSecond;

    public bool isGameInProgress = false;
    public bool isGamePause = false;
    public bool isGameOver = false;

    [Header("-UI Text-")]
    public TextMeshProUGUI timerText;

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

        StartCoroutine(TimerUpdate());

        EnemySpawner.instance.canMobSpawn = true;
        EnemySpawner.instance.SpawnStart();
        SoundManager.instance.PlayGameBGM(isGameInProgress);
    }

    public void TextHandleUpdate()
    {
        timerText.text = timerSecond.ToString();
    }

    public IEnumerator TimerUpdate()
    {
        while (isGameInProgress)
        {
            yield return new WaitForSeconds(1);
            timerSecond++;
            TextHandleUpdate();
        }
    }

    public void WeaponIconDisplay(int id)
    {
        for (int i = 0; i < weaponIcon.Length; i++)
        {
            weaponIcon[i].SetActive(false);
        }

        //weaponIcon[id].SetActive(true);
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
