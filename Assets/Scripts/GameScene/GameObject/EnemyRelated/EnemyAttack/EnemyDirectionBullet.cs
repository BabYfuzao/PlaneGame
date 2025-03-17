using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirectionBullet : EnemyBulletBase
{
    public void SetDirection(Vector2 dir)
    {
        Vector2 direction = dir;
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
