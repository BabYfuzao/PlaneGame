using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBBullet : PlayerBulletBase
{
    public SpriteRenderer sr;
    public Color[] bulletImages;

    public enum RGBBulletType
    {
        White,
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
            case RGBBulletType.White:
                sr.color = bulletImages[0];
                break;
            case RGBBulletType.Red:
                sr.color = bulletImages[1];
                break;
            case RGBBulletType.Green:
                sr.color = bulletImages[2];
                break;
            case RGBBulletType.Blue:
                sr.color = bulletImages[3];
                break;
            default:
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

            CheckEnemyBuff(enemy);

            GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void SetEnemyBuff(EnemyBase enemy)
    {
        foreach (var buffObj in enemy.rgbBuffObjs)
        {
            buffObj.SetActive(true);
        }

        switch (bulletType)
        {
            case RGBBulletType.Red:
                enemy.isRed = true;
                enemy.rgbBuffObjs[0].GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case RGBBulletType.Green:
                enemy.isGreen = true;
                enemy.rgbBuffObjs[0].GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case RGBBulletType.Blue:
                enemy.isBlue = true;
                enemy.rgbBuffObjs[0].GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            default:
                break;
        }
    }

    public void CheckEnemyBuff(EnemyBase enemy)
    {
        //Burn
        if (enemy.isRed && enemy.isGreen)
        {
            ResetBuff(enemy);
        }
        else if (enemy.isRed && enemy.isBlue)
        {
            ResetBuff(enemy);
        }
        //Palsy
        else if (enemy.isGreen && enemy.isBlue)
        {
            ResetBuff(enemy);
        }
    }

    public void ResetBuff(EnemyBase enemy)
    {
        enemy.isRed = false;
        enemy.isGreen = false;
        enemy.isBlue = false;

        foreach (var buffObj in enemy.rgbBuffObjs)
        {
            buffObj.GetComponent<SpriteRenderer>().color = Color.clear;
        }
    }
}
