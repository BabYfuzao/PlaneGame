using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GhostMobEnemy : MobEnemy
{
    public Collider2D collider2d;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(RandomHide());
    }

    public IEnumerator RandomHide()
    {
        while (canMove)
        {
            float randomTime = Random.Range(5f, 8f);

            yield return new WaitForSeconds(randomTime);
            StartCoroutine(SetVisibility(false));
            yield return new WaitForSeconds(3f);
            StartCoroutine(SetVisibility(true));
        }
    }

    private IEnumerator SetVisibility(bool isVisible)
    {
        float targetAlpha = isVisible ? 1f : 0f;

        yield return sr.DOFade(targetAlpha, 0.5f).SetEase(Ease.OutSine).WaitForCompletion();
        collider2d.enabled = isVisible;
    }

    protected override void Update()
    {
        base.Update();
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

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(0f, 0f), moveAreaSize);
    }
}
