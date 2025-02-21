using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EnemyBase : MonoBehaviour
{
    public SpriteRenderer sr;

    public int hP;
    protected HPBar hPBar;

    public float enterDistance;

    public bool canMove;

    public TextMeshPro dBHitCountText;
    public int dBHitCount;
    protected Vector3 originalPos;

    protected virtual void Start()
    {
        hPBar = GetComponentInChildren<HPBar>();
        hPBar.maxHP = hP;
        hPBar.currentHP = hPBar.maxHP;
        hPBar.UpdateHPBar();

        originalPos = transform.localPosition;

        CreateMovement();
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
        transform.DOLocalMoveX(originalPos.x - enterDistance, 1f);
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
            Destroy(gameObject);
        }
    }
}
