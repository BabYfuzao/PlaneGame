using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalBullet : PlayerBulletBase
{
    [HideInInspector]
    public DigitalWeapon weapon;

    public GameObject explosionPrefab;

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
        if (collision.gameObject.CompareTag("BulletRemover"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(atk);

            HitCount hitCount = collision.GetComponentInChildren<HitCount>();

            if (hitCount == null)
            {
                Vector3 spawnPosition = collision.transform.position;
                spawnPosition.y += 0.8f;

                GameObject hitCountObj = Instantiate(weapon.hitCountPrefab, spawnPosition, Quaternion.identity);
                hitCount = hitCountObj.GetComponent<HitCount>();
                hitCountObj.transform.SetParent(collision.transform);
            }
            hitCount.HitCountUpdate(1);

            CheckEnemyHitCount(enemy, hitCount, collision.gameObject);

            EnergyManager.instance.ReloadEnergy(weapon, 1);

            GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Initialize(DigitalWeapon weaponInstance)
    {
        weapon = weaponInstance;
    }

    public void CheckEnemyHitCount(EnemyBase enemy, HitCount hitCount , GameObject enemyObj)
    {
        if (hitCount.hitCount >= 8)
        {
            GameObject explosionObj = Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
            Explosion explosion = explosionObj.GetComponent<Explosion>();
            explosion.Initialize(this);
            explosionObj.GetComponent<Explosion>().StartExplosion();
            hitCount.HitCountUpdate(-8);
        }
    }
}
