using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalBullet : PlayerBulletBase
{
    public GameObject explosionPrefab;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, 1000f * Time.deltaTime);
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
            enemy.TakeDamage(atk);
            enemy.dBHitCount++;
            CheckEnemyHitCount(enemy);

            GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void CheckEnemyHitCount(EnemyBase enemy)
    {
        if (enemy.dBHitCount >= 10)
        {
            GameObject explosionObj = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
