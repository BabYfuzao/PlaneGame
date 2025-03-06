using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBase : MonoBehaviour
{
    public Rigidbody2D rb;
    private WeaponBase weapon;

    public int atk;
    public float moveSpeed;

    public GameObject enemyHitVFXPrefab;

    protected virtual void Start()
    {
        Vector2 direction = Vector2.right;
        rb.velocity = direction * moveSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletRemover"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(atk);
            GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Initialize(WeaponBase weaponInstance)
    {
        weapon = weaponInstance;
    }
}
