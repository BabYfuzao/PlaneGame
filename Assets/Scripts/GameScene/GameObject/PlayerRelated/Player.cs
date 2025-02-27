using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public WeaponBase weapon;
    public HPBar hPBar;

    public int hP;
    public float moveSpeed;

    private void Start()
    {
        hPBar.maxHP = hP;
        hPBar.currentHP = hPBar.maxHP;
        hPBar.UpdateHPBar();
    }
    void Update()
    {
        PlayerMove();
        BulletShoot();
    }

    public void PlayerMove()
    {
        //Player move
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (transform.localPosition.x <= -8)
        {
            move.x = Mathf.Max(0, move.x);
        }
        else if (transform.localPosition.x >= 8)
        {
            move.x = Mathf.Min(0, move.x);
        }

        if (transform.localPosition.y <= -4.5)
        {
            move.y = Mathf.Max(0, move.y);
        }
        else if (transform.localPosition.y >= 4.5)
        {
            move.y = Mathf.Min(0, move.y);
        }

        rb.velocity = move * moveSpeed;
    }

    public void BulletShoot()
    {
        //Bullet shoot
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(weapon.BulletShoot());
        }
    }

    public void TakeDamage(int damage)
    {
        hP -= damage;
        hPBar.SetHPBar(-damage);
        StartCoroutine(HitEffect());
        CheckDead();
    }

    public IEnumerator HitEffect()
    {
        sr.color = Color.gray;
        yield return new WaitForSeconds(0.2f);
        sr.color = Color.white;
    }

    public void CheckDead()
    {
        if (hP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
