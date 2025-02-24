using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using PathCreation.Examples;
using System;

public class EnemyBase : MonoBehaviour
{
    public SpriteRenderer sr;

    protected PathFollower pathFollower;

    public bool isBoss;

    public int hP;
    protected HPBar hPBar;

    public float enterDistance;
    public float enterDelay;

    public TextMeshPro dBHitCountText;
    public int dBHitCount;

    public bool isRed, isGreen, isBlue;

    protected Vector3 originalPos;

    public event Action<EnemyBase> OnDeath;

    protected virtual void Start()
    {
        hPBar = GetComponentInChildren<HPBar>();
        pathFollower = GetComponentInChildren<PathFollower>();

        hPBar.maxHP = hP;
        hPBar.currentHP = hPBar.maxHP;
        hPBar.UpdateHPBar();

        originalPos = transform.localPosition;

        //CreateMovement();
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

    protected virtual void CreateMovement()
    {
        transform.DOLocalMoveX(originalPos.x - enterDistance, 1f).OnComplete(() =>
        {
            StartCoroutine(pathFollower.StartFollow(enterDelay));
        });
    }

    public virtual void TakeDamage(int damage)
    {
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
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
