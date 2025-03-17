using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon2Enemy : EliteEnemy
{
    protected override void Start()
    {
        base.Start();
    }

    public override IEnumerator EliteAppear()
    {
        yield return MoveToPosition(move1Pos);
    }

    public override IEnumerator BulletFire()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireCD);

        Vector2 initialDirection = -transform.right;
        float angleStep = 60f / 4;
        float startAngle = -30f;

        for (int i = 0; i < 5; i++)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            EnemyDirectionBullet bullet = bulletObj.GetComponent<EnemyDirectionBullet>();

            float angle = startAngle + (angleStep * i);
            Vector2 dir = Quaternion.Euler(0, 0, angle) * initialDirection;

            bullet.SetDirection(dir);
        }

        canShoot = true;
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
}
