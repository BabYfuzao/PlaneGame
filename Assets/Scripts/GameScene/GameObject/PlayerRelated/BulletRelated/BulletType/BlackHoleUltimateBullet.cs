using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleUltimateBullet : BlackHoleBullet
{
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
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            weapon.BlackHoleInstantiate(collision.gameObject.transform.position);
        }
    }
}
