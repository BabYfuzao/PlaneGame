using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public float shootCD;
    public float shootTimer;
    public int Attack;
    public GameObject bulletPrefab;

    public void Start()
    {
        shootTimer = shootCD;
    }

    public virtual void BulletShoot()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootCD)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            shootTimer = 0f;
        }
    }
}