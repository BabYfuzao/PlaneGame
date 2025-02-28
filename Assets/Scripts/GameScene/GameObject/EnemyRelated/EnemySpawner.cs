using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public GameObject[] enemyPrefabs;
    public int waveIndex;
    public bool isBossWave;
}

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public EnemyWave[] enemyWaves;
    public int currentWave;
    public int enemyRemainCount;
    private bool isBossSpawned = false;

    private void Awake()
    {
        instance = this;
    }

    public void EnemySpawn()
    {
        currentWave++;

        if (enemyWaves[currentWave].isBossWave && !isBossSpawned)
        {
            isBossSpawned = true;
        }

        for (int i = 0; i < enemyWaves[currentWave].enemyPrefabs.Length; i++)
        {
            enemyWaves[currentWave].enemyPrefabs[i].SetActive(true);
            enemyRemainCount++;
        }
    }

    public void EnemyCountUpdate()
    {
        enemyRemainCount--;
        CheckAllEnemyDefeated();
    }

    public void CheckAllEnemyDefeated()
    {
        if (enemyRemainCount <= 0)
        {
            EnemySpawn();
        }
    }
}
