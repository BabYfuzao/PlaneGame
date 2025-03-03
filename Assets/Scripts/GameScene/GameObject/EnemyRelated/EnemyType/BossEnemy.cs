using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.TestTools;

public class BossEnemy : EnemyBase
{
    public Transform startPos;
    public Transform move1Pos;
    public Transform move2Pos;

    public GameObject bulletPrefab;
    public float shootCD;
    public bool canAttack;

    public float moveSpeed;
    public float moveRange;

    protected override void Start()
    {
        base.Start();
        canMove = false;
        StartCoroutine(BossAppear());
    }

    public IEnumerator BossAppear()
    {
        yield return transform.DOMove(startPos.position, moveSpeed).SetEase(Ease.InOutSine).WaitForCompletion();

        canMove = true;
        canAttack = true;

        StartCoroutine(StartMove());
    }

    public IEnumerator StartMove()
    {
        while (canMove)
        {
            yield return transform.DOMove(move1Pos.position, moveSpeed).SetEase(Ease.Linear).WaitForCompletion();

            yield return transform.DOMove(move2Pos.position, moveSpeed).SetEase(Ease.Linear).WaitForCompletion();
        }
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

    public IEnumerator BulletShoot()
    {
        while (canAttack)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(shootCD);
        }
    }
}