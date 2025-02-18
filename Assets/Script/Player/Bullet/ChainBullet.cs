using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBullet : PlayerBulletBase
{
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
        if (collision.gameObject.CompareTag("BulletRemover"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(atk);
            GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
