using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShooterMobEnemy : MobEnemy
{
    public GameObject bulletPrefab;
    public bool canFire;
    public float fireCD;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (canMove)
        {
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = GenerateNewTarget();
            }
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        if (canFire)
        {
            StartCoroutine(BulletFire());
        }
    }

    public IEnumerator BulletFire()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        canFire = false;
        yield return new WaitForSeconds(fireCD);
        canFire = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            SoundManager.instance.PlayEnemyHitSFX();
        }
        else if (collision.CompareTag("EnemyRemover"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(0f, 0f), moveAreaSize);
    }
}
