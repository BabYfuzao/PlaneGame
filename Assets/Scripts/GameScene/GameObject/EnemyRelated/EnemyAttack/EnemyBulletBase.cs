using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    public Rigidbody2D rb;

    public int atk;
    public float moveSpeed;

    public GameObject playerHitVFXPrefab;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
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
