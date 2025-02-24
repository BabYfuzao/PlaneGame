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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EnemyRemover")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("BlackHole"))
        {
            pathFollower.canMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BlackHole")
        {
            pathFollower.canMove = true;
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
