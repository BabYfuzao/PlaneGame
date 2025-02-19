using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public List<GameObject> enemyLevels;
    private int i = -1;
    public bool isBossSpawn = false;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyLevel"))
        {
            ActiveEnemySpawner();
        }
    }

    private void ActiveEnemySpawner()
    {
        i++;
        if (i < enemyLevels.Count)
        {
            GameObject enemyLevel = enemyLevels[i];
            enemyLevel.SetActive(true);

            if (enemyLevel.name == "BossLevel")
            {
                isBossSpawn = true;
            }
        }
    }
}
