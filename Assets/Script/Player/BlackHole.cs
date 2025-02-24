using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlackHole : MonoBehaviour
{
    public SpriteRenderer sr;

    public float attractionRange;
    public float attractionSpeed;

    private List<EnemyBase> attractedEnemies = new List<EnemyBase>();

    public void BlackHoleDuration(float durationTime)
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        transform.localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();

        sequence.Join(sr.DOFade(1f, 1f));
        sequence.Join(transform.DOScale(Vector3.one, 1f));

        sequence.OnComplete(() =>
        {
            StartCoroutine(FadeOutAndDestroy(durationTime));
        });
    }

    private IEnumerator FadeOutAndDestroy(float durationTime)
    {
        yield return new WaitForSeconds(durationTime);

        Sequence fadeOutSequence = DOTween.Sequence();

        fadeOutSequence.Join(sr.DOFade(0f, 1f));
        fadeOutSequence.Join(transform.DOScale(Vector3.zero, 1f));

        fadeOutSequence.OnComplete(() =>
        {
            GameContoller.instance.isBlackHoleSpawn = false;
            Destroy(gameObject);
        });
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, 100f * Time.deltaTime);
        AttractEnemies();
    }

    private void AttractEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attractionRange);

        foreach (var collider in enemies)
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyBase enemy = collider.GetComponent<EnemyBase>();
                attractedEnemies.Add(enemy);
                Vector2 direction = (transform.position - enemy.transform.position).normalized;
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                rb.MovePosition(rb.position + direction * attractionSpeed * Time.deltaTime);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attractionRange);
    }
}
