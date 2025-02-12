using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody2D rb;

    public int atk;

    public GameObject enemyHitVFXPrefab;

    private void Start()
    {
        Vector2 direction = Vector2.right;
        rb.velocity = direction * 15f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
