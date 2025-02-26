using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class Boss : EnemyBase
{
    public Transform movePos;

    public Transform attackStartPos;
    public Transform attackEndPos;

    public GameObject bossBulletPrefab;
    public GameObject bossBombPrefab;
    public float shootCD;

    public bool isShootMode = false;

    public bool isBombMode = false;
    public float bombDurationTime;

    protected override void Start()
    {
        base.Start();
    }

    public override IEnumerator StartMove()
    {
        transform.DOMove(movePos.position, delay).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(delay);

        pathFollower.canMove = true;
        isShootMode = true;
        yield return new WaitForSeconds(delay);
        StartCoroutine(BulletShoot());
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

    public void RandomAction()
    {
        isShootMode = false;
        isBombMode = false;

        if (Random.Range(0, 10) % 2 == 0)
        {
            pathFollower.canMove = true;
            isShootMode = true;
            StartCoroutine(BulletShoot());
        }
        else
        {
            pathFollower.canMove = false;
            isBombMode = true;
            StartCoroutine(BombShoot());
        }
    }

    public IEnumerator BulletShoot()
    {
        if (isShootMode)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject bullet = Instantiate(bossBulletPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(shootCD);
            }
            RandomAction();
        }
    }

    public IEnumerator BombShoot()
    {
        if (isBombMode)
        {
            transform.DOMove(attackStartPos.position, delay).SetEase(Ease.OutCubic);
            yield return new WaitForSeconds(2f);

            transform.DOMove(attackEndPos.position, bombDurationTime).SetEase(Ease.OutCubic);
            GameObject bomb = Instantiate(bossBombPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(bombDurationTime);
            RandomAction();
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