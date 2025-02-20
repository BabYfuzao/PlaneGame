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
            enemy.dBHitCountText.gameObject.SetActive(true);
            enemy.HitCountUpdate(1);
            CheckEnemyHitCount(enemy, collision.gameObject);

            GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public virtual void CheckEnemyHitCount(EnemyBase enemy, GameObject enemyObj)
    {
        int hitCountMax = 8;

        if (enemy.dBHitCount >= hitCountMax)
        {
            GameObject explosionObj = Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
            explosionObj.GetComponent<Explosion>().StartExplosion();
            enemy.HitCountUpdate(-hitCountMax);
        }
    }
}
