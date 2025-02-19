using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalBullet : PlayerBulletBase
{
    public int hitCount;

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
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(atk);
            GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
