using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBullet : PlayerBulletBase
{
    public GameObject blackHolePrefab;
    public float blackHoleDurationTime;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletRemover"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(atk);
            GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            GameObject blackHole = Instantiate(blackHolePrefab, transform.position, Quaternion.identity);
            blackHole.GetComponent<BlackHole>().BlackHoleDuration(blackHoleDurationTime);
            Destroy(gameObject);
        }
    }
}
