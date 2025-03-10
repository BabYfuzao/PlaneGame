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
    public SmoothBar hPBar;

    [SerializeField] private Vector2 startPos = Vector2.zero;
    [SerializeField] private Vector2 move1Pos = new Vector2(-5f, 0f);
    [SerializeField] private Vector2 move2Pos = new Vector2(5f, 0f);

    public GameObject bulletPrefab;
    public float shootCD;
    public bool canAttack;

    public float moveSpeed;
    public float moveRange;

    private void Start()
    {
        hPBar.maxValue = hP;
        hPBar.currentValue = hPBar.maxValue;
        hPBar.SetBar(hP);

        canMove = false;
        StartCoroutine(BossAppear());
    }

    public IEnumerator BossAppear()
    {
        yield return transform.DOMove(startPos, moveSpeed).SetEase(Ease.InOutSine).WaitForCompletion();

        canMove = true;
        canAttack = true;

        StartCoroutine(StartMove());
    }

    public IEnumerator StartMove()
    {
        while (canMove)
        {
            yield return transform.DOMove(move1Pos, moveSpeed).SetEase(Ease.Linear).WaitForCompletion();

            yield return transform.DOMove(move2Pos, moveSpeed).SetEase(Ease.Linear).WaitForCompletion();
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
        if (isWeak)
        {
            damage += 1;
        }

        hP -= damage;
        hPBar.SetBar(hP);
        CheckDead();
        StartCoroutine(HitEffect());
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