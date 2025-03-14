using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;
using TMPro;

public class MobEnemy : EnemyBase
{
    [Header("-Random Movement Settings-")]
    public Vector2 moveAreaSize;
    public float moveSpeed;
    public float maxMoveDistance;
    public float minMoveDistance;

    protected Vector2 targetPosition;

    protected override void Start()
    {
        base.Start();
        targetPosition = transform.position;
    }

    protected virtual void Update()
    {
        if (canMove)
        {
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = GenerateNewTarget();
            }
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    protected Vector2 GenerateNewTarget()
    {
        Vector2 currentPos = transform.position;
        Vector2 leftDirection = Vector2.left;

        float angle = Random.Range(-90f, 90f);
        Vector2 moveDirection = Quaternion.Euler(0, 0, angle) * leftDirection;

        float distance = Random.Range(minMoveDistance, maxMoveDistance);
        Vector2 newTarget = currentPos + moveDirection * distance;

        float minX = -moveAreaSize.x / 2f;
        float maxX = moveAreaSize.x / 2f;
        float minY = -moveAreaSize.y / 2f;
        float maxY = moveAreaSize.y / 2f;

        newTarget.x = Mathf.Clamp(newTarget.x, minX, maxX);
        newTarget.y = Mathf.Clamp(newTarget.y, minY, maxY);

        return newTarget;
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

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(0f, 0f), moveAreaSize);
    }
}