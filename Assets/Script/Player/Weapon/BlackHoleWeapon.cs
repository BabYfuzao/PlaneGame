using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleWeapon : WeaponBase
{
    public override IEnumerator BulletShoot()
    {
        if (canShoot)
        {
            SoundManager.instance.PlayBHBulletShootSFX();
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            canShoot = false;
            yield return new WaitForSeconds(shootCD);
            canShoot = true;
        }
    }
}
