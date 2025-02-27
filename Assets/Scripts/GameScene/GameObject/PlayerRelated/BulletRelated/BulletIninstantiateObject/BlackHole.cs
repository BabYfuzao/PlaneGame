using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlackHole : MonoBehaviour
{
    public SpriteRenderer sr;

    private List<EnemyBase> attractedEnemies = new List<EnemyBase>();
    private List<Vector2> originalPositions = new List<Vector2>();

    private void Start()
    {
        SoundManager.instance.PlayBHSFX();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, 100f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            EnemyBase enemy = collider.GetComponent<EnemyBase>();

            if (!enemy.isBoss)
            {
                if (!attractedEnemies.Contains(enemy))
                {
                    enemy.pathFollower.canMove = false;
                    attractedEnemies.Add(enemy);
                    originalPositions.Add(enemy.transform.position);
                    enemy.OnDeath += HandleEnemyDeath;
                }

                float randomX = Random.Range(-0.5f, 0.5f);
                float randomY = Random.Range(-0.5f, 0.5f);

                Vector2 targetPosition = (Vector2)transform.position + new Vector2(randomX, randomY);
                enemy.transform.DOMove(targetPosition, 2f).SetEase(Ease.OutCubic);
            }
        }
    }

    private void HandleEnemyDeath(EnemyBase enemy)
    {
        int index = attractedEnemies.IndexOf(enemy);
        if (index != -1)
        {
            attractedEnemies.RemoveAt(index);
            originalPositions.RemoveAt(index);
        }
    }

    public void BlackHoleDuration(float durationTime)
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        transform.localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();

        sequence.Join(sr.DOFade(1f, 1f));
        sequence.Join(transform.DOScale(Vector3.one, 1f));

        sequence.OnComplete(() =>
        {
            StartCoroutine(BlackHoleDestroy(durationTime));
        });
    }

    private IEnumerator BlackHoleDestroy(float durationTime)
    {
        yield return new WaitForSeconds(durationTime);

        ResetEnemies();
        Sequence fadeOutSequence = DOTween.Sequence();

        fadeOutSequence.Join(sr.DOFade(0f, 1.5f));
        fadeOutSequence.Join(transform.DOScale(Vector3.zero, 1.5f));

        fadeOutSequence.OnComplete(() =>
        {
            foreach (EnemyBase enemy in attractedEnemies)
            {
                enemy.pathFollower.canMove = true;
            }
            attractedEnemies.Clear();
            originalPositions.Clear();

            GameController.instance.isBlackHoleSpawn = false;
            Destroy(gameObject);
        });
    }

    private void ResetEnemies()
    {
        for (int i = 0; i < attractedEnemies.Count; i++)
        {
            EnemyBase enemy = attractedEnemies[i];
            Vector2 originalPosition = originalPositions[i];

            enemy.transform.DOMove(originalPosition, 1f).SetEase(Ease.OutCubic);
        }
    }
}
