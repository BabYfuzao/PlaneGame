using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public GameObject[] enemyPrefabs;
    public int needKillCount;
    public int waveIndex;
    public bool isBossWave;
}

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [Header("-Enemy Prefab-")]
    public EnemyWave[] enemyWaves;
    public GameObject mobEnemyPrefab;

    [Header("-Mob Enemy Spawn Area-")]
    public Vector2 spawnAreaSize;

    [Header("Spawner Setting")]
    public float spawnCD;
    public float reducedSpawnCD;
    public float minSpawnCD;

    [Header("-Spawner Value-")]
    public int currentWave;
    public int mobEnemyKillCount;

    [Header("-Spawner Status-")]
    public bool canMobSpawn = false;
    public bool isBossSpawned = false;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnStart()
    {
        StartCoroutine(MobEnemyRoamSpawn());
    }

    public IEnumerator MobEnemyRoamSpawn()
    {
        while (canMobSpawn)
        {
            Vector2 spawnPosition = GetRandomSpawnInArea();
            Instantiate(mobEnemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnCD);

            if (spawnCD >= minSpawnCD)
            {
                spawnCD -= reducedSpawnCD;
            }
        }
    }

    public Vector3 GetRandomSpawnInArea()
    {
        float x = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float y = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);

        return transform.position + new Vector3(x, y, 0);
    }

    public void EnemyWaveSpawn()
    {
        currentWave++;

        if (enemyWaves[currentWave - 1].isBossWave && !isBossSpawned)
        {
            isBossSpawned = true;
        }

        for (int i = 0; i < enemyWaves[currentWave - 1].enemyPrefabs.Length; i++)
        {
            enemyWaves[currentWave - 1].enemyPrefabs[i].SetActive(true);
        }
    }

    public void EnemyCountUpdate()
    {
        CheckAllEnemyDefeated();
    }

    public void CheckAllEnemyDefeated()
    {
        EnemyWaveSpawn();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector2(spawnAreaSize.x, spawnAreaSize.y));
    }
}
