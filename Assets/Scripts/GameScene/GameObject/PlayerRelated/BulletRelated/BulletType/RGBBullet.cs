using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBBullet : PlayerBulletBase
{
    public SpriteRenderer sr;
    public TrailRenderer tr;

    private RGBWeapon weapon;

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
        SetBullet(bulletType);

        float randomScale = Random.Range(0.8f, 1.2f);
        transform.localScale = new Vector3(randomScale, randomScale, 1);

        float randomRotation = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, 0, randomRotation);
    }

    private void SetBullet(RGBBulletType type)
    {
        switch (type)
        {
            case RGBBulletType.Red:
                sr.color = Color.red;
                tr.startColor = Color.red;
                tr.endColor = new Color(1, 0, 0, 0);
                break;
            case RGBBulletType.Green:
                sr.color = Color.green;
                tr.startColor = Color.green;
                tr.endColor = new Color(0, 1, 0, 0);
                break;
            case RGBBulletType.Blue:
                sr.color = Color.blue;
                tr.startColor = Color.blue;
                tr.endColor = new Color(0, 0, 1, 0);
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

            RGBBuff rgbBuff = collision.GetComponentInChildren<RGBBuff>();

            if (rgbBuff == null)
            {
                Vector3 spawnPosition = collision.transform.position;
                spawnPosition.y += 0.8f;

                GameObject rgbBuffObj = Instantiate(weapon.rgbBuffPrefab, spawnPosition, Quaternion.identity);
                rgbBuff = rgbBuffObj.GetComponent<RGBBuff>();
                rgbBuff.Initialize(enemy);

                rgbBuffObj.transform.SetParent(collision.transform);
            }
            rgbBuff.SetBuff(bulletType);

            GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            ParticleSystem.MainModule data = enemyHitVFX.GetComponent<ParticleSystem>().main;
            data.startColor = sr.color;
            Destroy(gameObject);
        }
    }
}
