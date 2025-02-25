using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Boss : EnemyBase
{
    public GameObject enemyBulletPrefab;
    public float shootCD;
    public bool canShoot = true;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        StartCoroutine(BulletShoot());
    }

    public override void StartMove()
    {
        base.StartMove();
    }

    public override void HitCountUpdate(int hitCount)
    {
        base.HitCountUpdate(hitCount);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EnemyRemover")
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("PlayerBullet"))
        {
            SoundManager.instance.PlayEnemyHitSFX();
        }
    }

    public IEnumerator BulletShoot()
    {
        if (canShoot)
        {
            GameObject enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            canShoot = false;
            yield return new WaitForSeconds(shootCD);
            canShoot = true;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    public override IEnumerator HitEffect()
    {
        yield return base.HitEffect();
    }

    protected override void CheckDead()
    {
        if (hP <= 0)
        {
            Destroy(gameObject);
            GameController.instance.GameOver();
        }
    }
}