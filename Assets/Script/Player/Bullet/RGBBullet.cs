using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBBullet : PlayerBulletBase
{
    public SpriteRenderer sr;
    private TrailRenderer trailRenderer;

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
        trailRenderer = GetComponent<TrailRenderer>();

        bulletType = (RGBBulletType)Random.Range(0, System.Enum.GetValues(typeof(RGBBulletType)).Length);
        SetBulletSprite(bulletType);
        SetTrailColor(bulletType);

        float randomScale = Random.Range(0.8f, 1.2f);
        transform.localScale = new Vector3(randomScale, randomScale, 1);

        float randomRotation = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, 0, randomRotation);
    }

    private void SetTrailColor(RGBBulletType type)
    {
        if (trailRenderer != null)
        {
            switch (type)
            {
                case RGBBulletType.Red:
                    trailRenderer.startColor = Color.red;
                    trailRenderer.endColor = new Color(1, 0, 0, 0);
                    break;
                case RGBBulletType.Green:
                    trailRenderer.startColor = Color.green;
                    trailRenderer.endColor = new Color(0, 1, 0, 0);
                    break;
                case RGBBulletType.Blue:
                    trailRenderer.startColor = Color.blue;
                    trailRenderer.endColor = new Color(0, 0, 1, 0);
                    break;
                default:
                    break;
            }
        }
    }

    private void SetBulletSprite(RGBBulletType type)
    {
        switch (type)
        {
            case RGBBulletType.Red:
                sr.color = Color.red;
                break;
            case RGBBulletType.Green:
                sr.color = Color.green;
                break;
            case RGBBulletType.Blue:
                sr.color = Color.blue;
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

            if (!enemy.isBuff)
            {
                RGBWeapon weapon = FindObjectOfType<RGBWeapon>();
                weapon.SetEnemyBuff(enemy, bulletType);
            }

            GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            ParticleSystem.MainModule data = enemyHitVFX.GetComponent<ParticleSystem>().main;
            data.startColor = sr.color;
            Destroy(gameObject);
        }
    }
}
