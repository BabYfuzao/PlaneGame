using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RGBWeapon : WeaponBase
{
    public GameObject rgbBuffPrefab;

    public GameObject ultBulletPrefab;

    public float ultDurationTime = 10;
    public bool canUltShoot;

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
        UltModeChange(true);
        StartCoroutine(UltBulletShoot());
    }

    public IEnumerator UltBulletShoot()
    {
        while (ultDurationTime > 0)
        {
            if (canUltShoot)
            {
                GameObject bulletObj = Instantiate(ultBulletPrefab, transform.position, Quaternion.identity);
                RGBBullet bullet = bulletObj.GetComponent<RGBBullet>();
                bullet.Initialize(this);

                canUltShoot = false;
                yield return new WaitForSeconds(shootCD);
                canUltShoot = true;
            }
            yield return new WaitForSeconds(1f);
            ultDurationTime--;
        }
        EnergyManager.instance.ResetEnergy();
        UltModeChange(false);
    }

    public void UltModeChange(bool isChange)
    {
        canShoot = !isChange;
        canUltShoot = isChange;
    }
}
