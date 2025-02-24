using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class BlackHoleBullet : PlayerBulletBase
{
    public GameObject blackHolePrefab;
    public float blackHoleDurationTime;
    public float probability;
    private HashSet<EnemyBase> damagedEnemies = new HashSet<EnemyBase>();

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BulletRemover")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();

            if (!damagedEnemies.Contains(enemy))
            {
                enemy.TakeDamage(atk);
                damagedEnemies.Add(enemy);
                GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);

                BlackHoleInstantiate(enemy.transform);
            }
        }
    }

    public void BlackHoleInstantiate(Transform enemyPos)
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= probability && !GameContoller.instance.isBlackHoleSpawn)
        {
            GameObject blackHoleObj = Instantiate(blackHolePrefab, enemyPos.position, Quaternion.identity);
            blackHoleObj.GetComponent<BlackHole>().BlackHoleDuration(blackHoleDurationTime);
            GameContoller.instance.isBlackHoleSpawn = true;
        }
    }
}
