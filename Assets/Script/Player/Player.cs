using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    public float moveSpeed;

    public GameObject bulletPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        PlayerMove();
        BulletShoot();
    }

    public void PlayerMove()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (transform.localPosition.x <= -8)
        {
            move.x = Mathf.Max(0, move.x);
        }
        else if (transform.localPosition.x >= 8)
        {
            move.x = Mathf.Min(0, move.x);
        }
        else if (transform.localPosition.y <= -5)
        {
            move.y = Mathf.Max(0, move.y);
        }
        else if (transform.localPosition.y >= 5)
        {
            move.y = Mathf.Min(0, move.y);
        }

        rb.velocity = move * moveSpeed;
    }

    public void BulletShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Destroy(bullet, 2f);
        }
    }
}
