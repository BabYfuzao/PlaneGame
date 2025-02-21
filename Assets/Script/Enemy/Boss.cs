using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : EnemyBase
{
    public GameObject enemyBulletPrefab;
    public float shootCD;
    private bool canShoot = true;

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
        var sequence = DOTween.Sequence();

        transform.DOLocalMoveX(originalPos.x - moveDistance, 1f).OnComplete(() =>
        {
            StartCoroutine(BulletShoot());
        });

        sequence.Append(transform.DOLocalMoveY(originalPos.y + 3, 1f))
                .Append(transform.DOLocalMoveY(originalPos.y - 3, 1f))
                .SetLoops(-1, LoopType.Yoyo);
    }

    public IEnumerator BulletShoot()
    {
        while (canShoot)
        {
            GameObject enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(shootCD);
        }
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

    public override IEnumerator HitEffect()
    {
        yield return base.HitEffect();
    }

    protected override void CheckDead()
    {
        base.CheckDead();
    }
}
