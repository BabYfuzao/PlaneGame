using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBWeapon : WeaponBase
{
    public GameObject rgbBuffPrefab;

    protected override void Start()
    {
        base.Start();
    }

    public override IEnumerator BulletShoot()
    {
        if (canShoot)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            RGBBullet bullet = bulletObj.GetComponent<RGBBullet>();
            bullet.Initialize(this);

            canShoot = false;
            yield return new WaitForSeconds(shootCD);
            canShoot = true;
        }
    }

    public override void Ultimate()
    {
    }
}
