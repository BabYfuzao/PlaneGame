using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareWeapon : WeaponBase
{
    public override IEnumerator BulletShoot()
    {
        if (!canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            canShoot = true;
            yield return new WaitForSeconds(shootCD);
            canShoot = false;
        }
    }
}
