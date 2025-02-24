using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBBullet : PlayerBulletBase
{
    public SpriteRenderer sr;
    public Color[] bulletImages;

    public enum RGBBulletType
    {
        Red,
        Green,
        Blue
    };

    private RGBBulletType bulletType;

    protected override void Start()
    {
        base.Start();

        bulletType = (RGBBulletType)Random.Range(0, System.Enum.GetValues(typeof(RGBBulletType)).Length);
        SetBulletSprite(bulletType);
    }

    private void SetBulletSprite(RGBBulletType type)
    {
        switch (type)
        {
            case RGBBulletType.Red:
                sr.color = bulletImages[0];
                break;
            case RGBBulletType.Green:
                sr.color = bulletImages[1];
                break;
            case RGBBulletType.Blue:
                sr.color = bulletImages[2];
                break;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BulletRemover")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(atk);
            SetEnemyBuff(enemy);

            CheckEnemyDebuff(enemy);

            GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void SetEnemyBuff(EnemyBase enemy)
    {
        switch (bulletType)
        {
            case RGBBulletType.Red:
                enemy.isRed = true;
                break;
            case RGBBulletType.Green:
                enemy.isGreen = true;
                break;
            case RGBBulletType.Blue:
                enemy.isBlue = true;
                break;
        }
    }

    public void CheckEnemyDebuff(EnemyBase enemy)
    {
        //Burn
        if (enemy.isRed && enemy.isGreen)
        {
            enemy.isRed = false;
            enemy.isGreen = false;
        }
        else if (enemy.isRed && enemy.isBlue)
        {
            enemy.isRed = false;
            enemy.isBlue = false;
        }
        //Palsy
        else if (enemy.isGreen && enemy.isBlue)
        {
            enemy.isGreen = false;
            enemy.isBlue = false;
        }
    }
}
