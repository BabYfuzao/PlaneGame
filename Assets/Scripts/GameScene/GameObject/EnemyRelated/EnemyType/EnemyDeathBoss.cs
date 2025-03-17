using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDeathBoss : BossEnemy
{
    public GameObject summonPrefab;

    public GameObject bulletPrefab;

    public GameObject shadowPrefab;

    protected override void Start()
    {
        base.Start();
    }

    public override IEnumerator BossAppear()
    {
        yield return new WaitForSeconds(0.5f);
        yield return MoveToPosition(move1Pos);

        StartCoroutine(BossMove());
    }

    public override IEnumerator BossMove()
    {
        canMove = true;
        StartCoroutine(RandomAction());

        while (canMove)
        {
            yield return MoveToPosition(move1Pos);
            yield return MoveToPosition(move2Pos);
            yield return MoveToPosition(move3Pos);
            yield return MoveToPosition(move2Pos);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            SoundManager.instance.PlayEnemyHitSFX();
        }
        else if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
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
    }

    public override IEnumerator RandomAction()
    {
        float randomTime = Random.Range(3f, 5f);
        float randomChance = Random.Range(0f, 10f);

        yield return new WaitForSeconds(randomTime);

        canMove = false;
        DOTween.Pause(transform);

        if (randomChance <= 4f)
        {
            anim.SetTrigger("isCall");
            SummonCall();
        }
        else if (randomChance > 4f && randomChance <= 7f)
        {
            anim.SetTrigger("isAttack1");
            StartCoroutine(BulletAttack());
        }
        else
        {
            anim.SetTrigger("isAttack2");
            yield return new WaitForSeconds(1f);
            StartCoroutine(ShadowAttack());
        }

        StartCoroutine(BossMove());
    }

    public void SummonCall()
    {
        Vector2 spawnPosition = new Vector2(transform.position.x + 2, transform.position.y + 2);
        GameObject summonObj = Instantiate(summonPrefab, spawnPosition, Quaternion.identity);
        summonObj.transform.localScale = Vector3.zero;
        summonObj.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    public IEnumerator BulletAttack()
    {
        float[] evenPositions = { 4f, 2f, 0f, -2f, -4f };
        float[] oddPositions = { 3f, 1f, -1f, -3f };

        float[] yPositions = Random.Range(0, 2) == 0 ? evenPositions : oddPositions;

        for (int loop = 0; loop < 4; loop++)
        {
            if (loop % 2 == 0)
            {
                Array.Reverse(yPositions);
            }

            foreach (float y in yPositions)
            {
                Vector3 spawnPosition = new Vector3(4f, y, 0);
                GameObject bulletObj = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public IEnumerator ShadowAttack()
    {
        yield return sr.DOFade(0f, 0.5f).SetEase(Ease.OutBack).WaitForCompletion();

        Vector3[] spawnPositions = {
            new Vector2(10f, 6f),
            new Vector2(-10f, 6f),
            new Vector2(10f, -6f),
            new Vector2(-10f, -6f)
        };

        foreach (Vector3 position in spawnPositions)
        {
            GameObject shadowObj = Instantiate(shadowPrefab, position, Quaternion.identity);
            EnemyShadowAttack shadow = shadowObj.GetComponent<EnemyShadowAttack>();
            shadow.Initialize(this);
            shadow.AttackSet();
        }
    }

    public void ShadowAttackComplete()
    {
        StartCoroutine(Restore());
    }

    public IEnumerator Restore()
    {
        StartCoroutine(BossMove());
        yield return sr.DOFade(1f, 0.5f).SetEase(Ease.OutBack).WaitForCompletion();
    }
}
