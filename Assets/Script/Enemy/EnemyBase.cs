using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{
    public int hP;
    public float moveDistance;
    protected Vector3 originalPos;

    protected virtual void Start()
    {
        originalPos = transform.localPosition;

        CreateMovement();
    }

    protected virtual void CreateMovement()
    {
        transform.DOLocalMoveX(originalPos.x - moveDistance, 1f);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EnemyRemover")
        {
            Destroy(gameObject);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        hP -= damage;
        CheckDead();
    }

    protected virtual void CheckDead()
    {
        if (hP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
