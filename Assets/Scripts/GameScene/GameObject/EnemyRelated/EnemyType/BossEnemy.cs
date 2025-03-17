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

    public Vector2 move1Pos;
    public Vector2 move2Pos;
    public Vector2 move3Pos;

    public bool canAttack;
    public bool isDead;

    public GameObject portalPrefab;

    protected override void Start()
    {
        base.Start();

        hPBar.gameObject.SetActive(true);
        hPBar.maxValue = hP;
        hPBar.currentValue = hPBar.maxValue;
        hPBar.SetBar(hP);

        canMove = false;
        StartCoroutine(BossAppear());
    }

    public virtual IEnumerator BossAppear()
    {
        anim.SetBool("isAttack", true);
        yield return MoveToPosition(move1Pos);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isAttack", false);

        StartCoroutine(BossMove());
    }

    public virtual IEnumerator BossMove()
    {
        canMove = true;
        StartCoroutine(RandomAction());

        while (canMove)
        {
            yield return MoveToPosition(move2Pos);
            yield return MoveToPosition(move3Pos);
            yield return MoveToPosition(move1Pos);
        }
    }

    public IEnumerator MoveToPosition(Vector3 targetPosition)
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

    public virtual IEnumerator RandomAction()
    {
        float randomTime = Random.Range(5f, 10f);
        float randomChance = Random.Range(0f, 10f);

        yield return new WaitForSeconds(randomTime);

        canMove = false;
        DOTween.Pause(transform);

        StartCoroutine(BossMove());
    }

    protected override void CheckDead()
    {
        if (hP <= 0 && !isDead)
        {
            isDead = true;
            SoundManager.instance.PlayEnemyDeadSFX();
            anim.SetTrigger("isDead");
        }
    }

    public virtual void BossDeadControl()
    {
        GameController.instance.BossDefeat();

        GameObject portalObj = Instantiate(portalPrefab, transform.position, Quaternion.identity);

        Transform portalTransform = portalObj.transform;

        portalTransform.localScale = Vector3.zero;

        portalTransform.DOScale(Vector3.one, 1f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}