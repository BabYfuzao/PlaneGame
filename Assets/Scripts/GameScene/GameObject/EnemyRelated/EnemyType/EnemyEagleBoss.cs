using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyEagleBoss : BossEnemy
{
    public ShadowEffect shadowEffect;
    public Transform playerTransform;

    protected override void Start()
    {
        base.Start();
    }

    public override IEnumerator BossAppear()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttack", true);
        yield return StartCoroutine(DashAttack());
        anim.SetBool("isAttack", false);

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
        float randomTime = Random.Range(8f, 12f);
        float randomChance = Random.Range(0f, 10f);

        yield return new WaitForSeconds(randomTime);

        canMove = false;
        DOTween.Pause(transform);

        if (randomChance <= 10f)
        {
            yield return StartCoroutine(DashAttack());
        }

        StartCoroutine(BossMove());
    }

    public IEnumerator DashAttack()
    {
        anim.SetBool("isAttack", true);

        StartCoroutine(ShowWarningLine(new Vector2(12, transform.position.y), new Vector2(-12, transform.position.y), 0f));

        yield return sr.DOColor(Color.red, 1f).SetEase(Ease.OutCubic).WaitForCompletion();

        shadowEffect.StartShadowEffect(1);
        yield return transform.DOMove(new Vector2(-12, transform.position.y), 0.5f).SetEase(Ease.OutCubic).WaitForCompletion();
        shadowEffect.StopShadowEffect();

        yield return new WaitForSeconds(0.5f);
        transform.localScale = new Vector3(-1, 1, 1);
        shadowEffect.StartShadowEffect(-1);
        yield return transform.DOMove(new Vector2(6, transform.position.y), 0.5f).SetEase(Ease.OutCubic).WaitForCompletion();
        shadowEffect.StopShadowEffect();

        anim.SetBool("isAttack", false);
        yield return sr.DOColor(defaultColor, 1f).SetEase(Ease.OutCubic).WaitForCompletion();
        transform.localScale = new Vector3(1, 1, 1);

        HideWarningLine();
    }

    private IEnumerator ShowWarningLine(Vector2 start, Vector2 end, float duration)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        for (int i = 0; i < 15; i++)
        {
            lineRenderer.enabled = !lineRenderer.enabled;
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(HideLineAfterDuration(lineRenderer, duration));
    }

    private IEnumerator HideLineAfterDuration(LineRenderer lineRenderer, float duration)
    {
        yield return new WaitForSeconds(duration);
        lineRenderer.enabled = false;
    }

    private void HideWarningLine()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
}
