using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : EnemyBase
{
    protected override void Start()
    {
        base.Start();
    }

    public override void HitCountUpdate(int hitCount)
    {
        base.HitCountUpdate(hitCount);
    }

    protected override void CreateMovement()
    {
        base.CreateMovement();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EnemyRemover")
        {
            Destroy(gameObject);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void CheckDead()
    {
        base.CheckDead();
    }
}
