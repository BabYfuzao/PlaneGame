using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemyShadowAttack : MonoBehaviour
{
    public ShadowEffect shadowEffect;
    public Player player;
    private EnemyDeathBoss boss;
    public LineRenderer warningLine;
    public float warningDuration = 0.5f;
    private float blinkInterval = 0.1f;

    public float xLimit = 10f;
    public float yLimit = 6f;

    public int attackIndex;

    public int atk;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(atk);
        }
    }

    public void Initialize(EnemyDeathBoss bossInstance)
    {
        boss = bossInstance;
    }

    public void AttackSet()
    {
        boss.gameObject.SetActive(false);
        attackIndex++;
        if (attackIndex <= 3)
        {
            StartCoroutine(StartAttack());
            return;
        }
        boss.gameObject.SetActive(true);
        boss.ShadowAttackComplete();
        for (int i = 0; shadowEffect.shadowQueue.Count > 0; i++)
        {
            GameObject shadowObj = shadowEffect.shadowQueue.Dequeue();
            Destroy(shadowObj);
        }
        Destroy(gameObject);
    }

    public IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(1f);

        Vector2 playerPos = player.transform.position;
        Vector2 shadowPos = transform.position;

        Vector2 direction = (playerPos - shadowPos).normalized;
        Vector2 targetPosition = CalculateTargetPosition(shadowPos, direction);

        yield return StartCoroutine(ShowWarningLine(shadowPos, targetPosition));

        shadowEffect.StartShadowEffect(direction.x > 0 ? 1f : -1f);
        yield return MoveThroughPlayer(targetPosition);
    }

    private Vector2 CalculateTargetPosition(Vector2 startPos, Vector2 direction)
    {
        float xDistance = direction.x > 0 ?
            (xLimit - startPos.x) / direction.x :
            (-xLimit - startPos.x) / direction.x;

        float yDistance = direction.y > 0 ?
            (yLimit - startPos.y) / direction.y :
            (-yLimit - startPos.y) / direction.y;

        float distance = Mathf.Min(Mathf.Abs(xDistance), Mathf.Abs(yDistance));
        return startPos + direction * distance;
    }

    private IEnumerator MoveThroughPlayer(Vector2 targetPosition)
    {
        Tweener moveTween = transform.DOMove(targetPosition, 0.5f)
            .SetEase(Ease.Linear);

        yield return moveTween.WaitForCompletion();
        shadowEffect.StopShadowEffect();
        AttackSet();
    }

    private IEnumerator ShowWarningLine(Vector2 start, Vector2 end)
    {
        warningLine.enabled = true;
        warningLine.SetPosition(0, start);
        warningLine.SetPosition(1, end);

        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < warningDuration)
        {
            warningLine.enabled = isVisible;
            isVisible = !isVisible;
            elapsedTime += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        warningLine.enabled = false;
    }
}