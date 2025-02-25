using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Enemy : EnemyBase
{
    protected override void Start()
    {
        base.Start();
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
        if (collision.CompareTag("PlayerBullet"))
        {
            SoundManager.instance.PlayEnemyHitSFX();
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
        base.CheckDead();
    }
}