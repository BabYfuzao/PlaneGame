using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EliteEnemy : EnemyBase
{
    public Vector2 move1Pos;
    public Vector2 move2Pos;
    public Vector2 move3Pos;

    public GameObject bulletPrefab;
    public bool canShoot;
    public float fireCD;

    protected override void Start()
    {
        base.Start();
        canMove = false;
        StartCoroutine(EliteAppear());
    }

    private void Update()
    {
        if (canShoot)
        {
            StartCoroutine(BulletFire());
        }
    }

    public IEnumerator EliteAppear()
    {
        yield return MoveToPosition(move1Pos);

        StartCoroutine(BossMove());
    }

    public IEnumerator BossMove()
    {
        canMove = true;

        while (canMove)
        {
            yield return MoveToPosition(move2Pos);
            yield return MoveToPosition(move3Pos);
            yield return MoveToPosition(move2Pos);
            yield return MoveToPosition(move1Pos);
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        float duration = distance / moveSpeed;

        yield return transform.DOMove(targetPosition, duration).SetEase(Ease.Linear).WaitForCompletion();
    }

    public IEnumerator BulletFire()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireCD);
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        canShoot = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            SoundManager.instance.PlayEnemyHitSFX();
        }
        else if (collision.CompareTag("EnemyRemover"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}
