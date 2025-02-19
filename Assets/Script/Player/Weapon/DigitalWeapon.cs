using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalWeapon : WeaponBase
{
    public override IEnumerator BulletShoot()
    {
        if (!canShoot)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            DigitalBullet bullet = bulletObj.GetComponent<DigitalBullet>();
            canShoot = true;
            yield return new WaitForSeconds(shootCD);
            canShoot = false;
        }
    }
}
