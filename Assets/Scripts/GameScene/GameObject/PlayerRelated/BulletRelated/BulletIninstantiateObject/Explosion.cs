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
    private DigitalBullet dB;
    private HashSet<EnemyBase> damagedEnemies = new HashSet<EnemyBase>();

    public void Initialize(DigitalBullet bulletInstance)
    {
        dB = bulletInstance;
    }

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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SoundManager.instance.PlayExplosionSFX();
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();

            if (!damagedEnemies.Contains(enemy))
            {
                enemy.TakeDamage(atk);

                HitCount hitCount = collision.GetComponentInChildren<HitCount>();

                if (hitCount == null)
                {
                    Vector3 spawnPosition = collision.transform.position;
                    spawnPosition.y += 0.8f;

                    GameObject hitCountObj = Instantiate(dB.weapon.hitCountPrefab, spawnPosition, Quaternion.identity);
                    hitCount = hitCountObj.GetComponent<HitCount>();
                    hitCountObj.transform.SetParent(collision.transform);
                    hitCount.HitCountUpdate(1);
                }
                else
                {
                    hitCount.HitCountUpdate(1);
                }

                CheckEnemyHitCount(enemy, hitCount, collision.gameObject);

                GameObject enemyHitVFX = Instantiate(enemyHitVFXPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    public void CheckEnemyHitCount(EnemyBase enemy, HitCount hitCount, GameObject enemyObj)
    {
        if (hitCount.hitCount >= 8)
        {
            GameObject explosionObj = Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
            explosionObj.GetComponent<Explosion>().StartExplosion();
            hitCount.HitCountUpdate(-8);
        }
    }
}
