using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;

    void Update()
    {
        Vector2 direction = Vector2.right;
        rb.velocity = direction * 15f;
    }
}
