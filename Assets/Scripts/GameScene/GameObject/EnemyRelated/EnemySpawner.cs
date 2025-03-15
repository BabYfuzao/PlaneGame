using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EliteEnemyPrefab
{
    public GameObject enemyPrefab;
    public float spawnDelay;
}

[System.Serializable]
public class EnemyWave
{
    public EliteEnemyPrefab[] eliteEnemys;

    public int needKillCount;
    public int waveIndex;
    public bool isBossWave;
}

[System.Serializable]
public class MobEnemyGroup
{
    public GameObject mobEnemyPrefab;
    public float chance;
}

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [Header("-Enemy Prefab-")]
    public EnemyWave[] enemyWaves;
    public MobEnemyGroup[] mobEnemys;

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

            RandomMobEnemy(spawnPosition);

            yield return new WaitForSeconds(spawnCD);

            if (spawnCD >= minSpawnCD)
            {
                spawnCD -= reducedSpawnCD;
            }
        }
    }

    public void RandomMobEnemy(Vector3 spawnPosition)
    {
        float randomNum = Random.Range(0f, 1f);
        float cumulativeChance = 0f;

        foreach (var mob in mobEnemys)
        {
            cumulativeChance += mob.chance;
            if (randomNum <= cumulativeChance)
            {
                Instantiate(mob.mobEnemyPrefab, spawnPosition, Quaternion.identity);
                break;
            }
        }
    }

    public Vector3 GetRandomSpawnInArea()
    {
        float x = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float y = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);

        return transform.position + new Vector3(x, y, 0);
    }

    public void MobEnemyKillCountUpdate(int count)
    {
        mobEnemyKillCount += count;
        if (canWaveSpawn())
        {
            StartCoroutine(EnemyWaveSpawn());
        }
    }

    public bool canWaveSpawn()
    {
        if (currentWave < enemyWaves.Length)
        {
            return mobEnemyKillCount >= enemyWaves[currentWave].needKillCount;
        }
        return false;
    }

    public IEnumerator EnemyWaveSpawn()
    {
        currentWave++;

        if (enemyWaves[currentWave - 1].isBossWave && !isBossSpawned)
        {
            isBossSpawned = true;
        }

        for (int i = 0; i < enemyWaves[currentWave - 1].eliteEnemys.Length; i++)
        {
            yield return new WaitForSeconds(enemyWaves[currentWave - 1].eliteEnemys[i].spawnDelay);
            enemyWaves[currentWave - 1].eliteEnemys[i].enemyPrefab.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector2(spawnAreaSize.x, spawnAreaSize.y));
    }
}
