using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hP;

    public void TakeDamage(int damage)
    {
        hP -= damage;
        CheckDead();
    }

    public void CheckDead()
    {
        if (hP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
