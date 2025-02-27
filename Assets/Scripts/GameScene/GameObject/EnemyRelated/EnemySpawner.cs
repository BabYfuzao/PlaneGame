using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public List<GameObject> enemys;

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
        for(int i = 0; i < enemys.Count; i++)
        {
            GameObject enemy = enemys[i];
            enemy.SetActive(true);
            CameraMove.instance.canMove = false;
        }
    }
}
