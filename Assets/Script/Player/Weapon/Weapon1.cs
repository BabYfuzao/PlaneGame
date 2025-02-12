using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : WeaponBase
{
    public override void BulletShoot()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootCD)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Destroy(bullet, 2f);
            shootTimer = 0f;
        }
    }
}
