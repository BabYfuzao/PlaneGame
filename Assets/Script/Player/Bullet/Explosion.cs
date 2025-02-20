using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject enemyHitVFXPrefab;
    public int atk;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BulletRemover")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(atk);
            //enemy.dBHitCountText.gameObject.SetActive(true);
            enemy.dBHitCount++;
            CheckEnemyHitCount(enemy);

            GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.5f);
        }
    }

    public void CheckEnemyHitCount(EnemyBase enemy)
    {
        if (enemy.dBHitCount >= 10)
        {
            GameObject explosionObj = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            enemy.dBHitCount = 0;
        }
    }
}
