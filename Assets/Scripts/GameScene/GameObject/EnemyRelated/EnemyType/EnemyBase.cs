using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    [Header("-Component-")]
    public SpriteRenderer sr;

    [Header("-Enemy type-")]
    public bool isMob;
    public bool isElite;
    public bool isBoss;

    [Header("-Enemy status-")]
    public int hP;

    public bool isWeak;

    public bool canMove;

    //Other
    protected Vector3 originalPos;
    public Color defaultColor;
    public event Action<EnemyBase> OnDeath;

    protected virtual void Start()
    {
        defaultColor = sr.color;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            SoundManager.instance.PlayEnemyHitSFX();
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (isWeak)
        {
            damage += 1;
        }

        hP -= damage;
        CheckDead();
    }

    protected virtual void CheckDead()
    {
        if (hP <= 0)
        {
            SoundManager.instance.PlayEnemyDeadSFX();

            if (isMob)
            {
                EnemySpawner.instance.MobEnemyKillCountUpdate(1);
            }

            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
