using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Explosion : MonoBehaviour
{
    public SpriteRenderer sr;

    public GameObject explosionPrefab;
    public GameObject enemyHitVFXPrefab;
    public int atk;
    private HashSet<EnemyBase> damagedEnemies = new HashSet<EnemyBase>();

    public void StartExplosion()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        transform.localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();

        sequence.Join(sr.DOFade(0.5f, 0.2f));
        sequence.Join(transform.DOScale(Vector3.one, 0.2f));

        sequence.OnComplete(() =>
        {
            StartCoroutine(FadeOutAndDestroy());
        });
    }

    private IEnumerator FadeOutAndDestroy()
    {
        yield return new WaitForSeconds(0.1f);

        Sequence fadeOutSequence = DOTween.Sequence();

        fadeOutSequence.Join(sr.DOFade(0f, 0.2f));
        fadeOutSequence.Join(transform.DOScale(Vector3.zero, 0.2f));

        fadeOutSequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BulletRemover")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();

            if (!damagedEnemies.Contains(enemy))
            {
                enemy.TakeDamage(atk);
                enemy.dBHitCountText.gameObject.SetActive(true);
                enemy.HitCountUpdate(1);
                damagedEnemies.Add(enemy);

                GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);

                CheckEnemyHitCount(enemy);
            }
        }
    }

    public void CheckEnemyHitCount(EnemyBase enemy)
    {
        int hitCountMax = 8;

        if (enemy.dBHitCount >= hitCountMax)
        {
            GameObject explosionObj = Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
            explosionObj.GetComponent<Explosion>().StartExplosion();
            enemy.HitCountUpdate(-hitCountMax);
        }
    }
}
