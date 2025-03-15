using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyFungiKingBoss : BossEnemy
{
    public GameObject mobEnemyPrefab;

    public GameObject bulletPrefab;

    protected override void Start()
    {
        base.Start();
    }

    public override IEnumerator BossAppear()
    {
        anim.SetBool("isAttack", true);
        yield return MoveToPosition(move1Pos);
        anim.SetBool("isAttack", false);
        yield return new WaitForSeconds(2f);

        StartCoroutine(BossMove());
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
        float randomTime = Random.Range(5f, 10f);
        float randomChance = Random.Range(0f, 10f);

        yield return new WaitForSeconds(randomTime);

        canMove = false;
        DOTween.Pause(transform);

        if (randomChance <= 5)
        {
            yield return StartCoroutine(MobSpawn());
        }
        else
        {
            yield return StartCoroutine(BulletShoot());
        }

        StartCoroutine(BossMove());
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
