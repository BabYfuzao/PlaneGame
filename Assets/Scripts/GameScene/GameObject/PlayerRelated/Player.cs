using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("-Compponent-")]
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public WeaponBase weapon;
    public HPBar hPBar;

    [Header("-Player status-")]
    public int hP;
    public float moveSpeed;

    [Header("-Movement Area-")]
    public Vector2 moveAreaSize = new Vector2(16f, 9f);

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
        rb.velocity = move * moveSpeed;

        Vector2 newPosition = transform.localPosition;
        float minX = -moveAreaSize.x / 2f;
        float maxX = moveAreaSize.x / 2f;
        float minY = -moveAreaSize.y / 2f;
        float maxY = moveAreaSize.y / 2f;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.localPosition = newPosition;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(0f, 0f), new Vector2(moveAreaSize.x, moveAreaSize.y));
    }
}
