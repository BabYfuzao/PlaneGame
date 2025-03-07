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
    public SmoothBar hPBar;

    public bool isWeak;

    public bool canMove;

    //Other
    protected Vector3 originalPos;
    public event Action<EnemyBase> OnDeath;

    protected virtual void Start()
    {
        hPBar.maxValue = hP;
        hPBar.currentValue = hPBar.maxValue;
        hPBar.SetBar(hP);
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
        hPBar.SetBar(hP);
        CheckDead();
        StartCoroutine(HitEffect());
    }

    public virtual IEnumerator HitEffect()
    {
        sr.color = Color.gray;
        yield return new WaitForSeconds(0.2f);
        sr.color = Color.white;
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
