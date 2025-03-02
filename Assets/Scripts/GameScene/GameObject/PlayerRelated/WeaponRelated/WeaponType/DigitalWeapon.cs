using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalWeapon : WeaponBase
{
    public GameObject hitCountPrefab;

    public override IEnumerator BulletShoot()
    {
        if (canShoot)
        {
            SoundManager.instance.PlayDBulletShootSFX();
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            DigitalBullet bullet = bulletObj.GetComponent<DigitalBullet>();
            bullet.Initialize(this);

            canShoot = false;
            yield return new WaitForSeconds(shootCD);
            canShoot = true;
        }
    }
}
