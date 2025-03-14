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
    public Animator anim;
    public SmoothBar hPBar;
    public ShadowEffect shadowEffect;

    [SerializeField] private Vector2 startPos = Vector2.zero;
    [SerializeField] private Vector2 move1Pos = new Vector2(-5f, 0f);
    [SerializeField] private Vector2 move2Pos = new Vector2(5f, 0f);

    public GameObject mobEnemyPrefab;

    public GameObject bulletPrefab;
    public float shootCD;
    public bool canAttack;

    public float moveSpeed;
    public float moveRange;

    protected override void Start()
    {
        base.Start();

        hPBar.maxValue = hP;
        hPBar.currentValue = hPBar.maxValue;
        hPBar.SetBar(hP);

        hPBar.gameObject.SetActive(true);

        canMove = false;
        StartCoroutine(BossAppear());
    }

    public IEnumerator BossAppear()
    {
        anim.SetBool("isAttack", true);
        yield return MoveToPosition(startPos);
        anim.SetBool("isAttack", false);

        StartCoroutine(BossMove());
    }

    public IEnumerator BossMove()
    {
        canMove = true;
        StartCoroutine(RandomAction());

        while (canMove)
        {
            yield return MoveToPosition(move1Pos);
            yield return MoveToPosition(move2Pos);
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        float duration = distance / moveSpeed;

        yield return transform.DOMove(targetPosition, duration).SetEase(Ease.Linear).WaitForCompletion();
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
    }

    public IEnumerator RandomAction()
    {
        float randomTime = Random.Range(5f, 10f);
        float randomChance = Random.Range(0f, 10f);

        yield return new WaitForSeconds(randomTime);

        canMove = false;
        DOTween.Kill(transform);

        if (randomChance <= 1f)
        {
            yield return StartCoroutine(DashAttack());
        }
        else if (randomChance > 1 && randomChance <= 4)
        {
            yield return StartCoroutine(MobSpawn());
        }
        else
        {
            yield return StartCoroutine(BulletShoot());
        }

        StartCoroutine(BossMove());
    }

    public IEnumerator DashAttack()
    {
        yield return sr.DOColor(Color.red, 1f).SetEase(Ease.OutCubic).WaitForCompletion();
        yield return new WaitForSeconds(1f);
        anim.SetBool("isAttack", true);

        shadowEffect.StartShadowEffect(1);
        yield return transform.DOMove(new Vector2(-12, transform.position.y), 1f).SetEase(Ease.OutCubic).WaitForCompletion();
        shadowEffect.StopShadowEffect();

        yield return new WaitForSeconds(1f);
        transform.localScale = new Vector3(-1, 1, 1);
        shadowEffect.StartShadowEffect(-1);
        yield return transform.DOMove(new Vector2(6, transform.position.y), 1f).SetEase(Ease.OutCubic).WaitForCompletion();
        shadowEffect.StopShadowEffect();

        yield return sr.DOColor(defaultColor, 1f).SetEase(Ease.OutCubic).WaitForCompletion();
        transform.localScale = new Vector3(1, 1, 1);
        anim.SetBool("isAttack", false);
    }

    public IEnumerator BulletShoot()
    {
        yield return new WaitForSeconds(1f);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    public IEnumerator MobSpawn()
    {
        yield return sr.DOColor(Color.green, 1f).SetEase(Ease.OutCubic).WaitForCompletion();

        Vector2[] offsets = {
            new Vector2(0, 3),
            new Vector2(0, -3),
            new Vector2(3, 0),
            new Vector2(-3, 0)
        };

        foreach (var offset in offsets)
        {
            GameObject enemy = Instantiate(mobEnemyPrefab, (Vector2)transform.position + offset, Quaternion.identity);
            enemy.transform.localScale = Vector3.zero;
            enemy.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        }

        yield return sr.DOColor(defaultColor, 1f).SetEase(Ease.OutCubic).WaitForCompletion();
    }
}