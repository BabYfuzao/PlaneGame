using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalBullet : EnemyBulletBase
{
    void Start()
    {
        Vector2 direction = Vector2.left;
        rb.velocity = direction * moveSpeed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyRemover"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(atk);
            GameObject playerHitVFX = Instantiate(playerHitVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
