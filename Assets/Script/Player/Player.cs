using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public WeaponBase weapon;

    public float moveSpeed;

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
}
