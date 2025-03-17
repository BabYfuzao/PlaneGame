using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBullet : PlayerBulletBase
{
    [HideInInspector]
    public BlackHoleWeapon weapon;

    private HashSet<EnemyBase> damagedEnemies = new HashSet<EnemyBase>();

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

            if (!damagedEnemies.Contains(enemy))
            {
                enemy.TakeDamage(atk);
                damagedEnemies.Add(enemy);

                EnergyManager.instance.ReloadEnergy(weapon, 1);

                GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    public void Initialize(BlackHoleWeapon weaponInstance)
    {
        weapon = weaponInstance;
    }
}
