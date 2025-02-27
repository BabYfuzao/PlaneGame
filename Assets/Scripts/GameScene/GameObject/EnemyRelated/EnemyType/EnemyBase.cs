using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using PathCreation.Examples;
using System;
using System.Globalization;

public class EnemyBase : MonoBehaviour
{
    public SpriteRenderer sr;

    public PathFollower pathFollower;

    public bool isBoss;

    public int hP;
    protected HPBar hPBar;

    public float delay;

    public TextMeshPro dBHitCountText;
    public int dBHitCount;

    public bool isRed, isGreen, isBlue;
    public bool isWeak;
    public bool isBuff;
    public GameObject rgbBuffObj;

    protected Vector3 originalPos;

    public event Action<EnemyBase> OnDeath;

    protected virtual void Start()
    {
        hPBar = GetComponentInChildren<HPBar>();
        pathFollower = GetComponentInChildren<PathFollower>();

        hPBar.maxHP = hP;
        hPBar.currentHP = hPBar.maxHP;
        hPBar.UpdateHPBar();

        originalPos = transform.position;

        StartCoroutine(StartMove());
    }

    public virtual IEnumerator StartMove()
    {
        yield return new WaitForSeconds(delay);
        pathFollower.canMove = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            SoundManager.instance.PlayEnemyHitSFX();
        }
    }

    public virtual void HitCountUpdate(int hitCount)
    {
        dBHitCount += hitCount;

        if (dBHitCount <= 0)
        {
            dBHitCountText.gameObject.SetActive(false);
        }

        TextHandle();
    }

    public virtual void TextHandle()
    {
        dBHitCountText.text = dBHitCount.ToString();
    }

    public virtual void TakeDamage(int damage)
    {
        if (isWeak)
        {
            damage += 1;
        }

        hP -= damage;
        hPBar.SetHPBar(-damage);
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
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
