using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleWeapon : WeaponBase
{
    public GameObject bhUltBullet;
    public GameObject blackHolePrefab;
    public float blackHoleDurationTime;

    public bool isBlackHoleSpawn;

    protected override void Start()
    {
        base.Start();
    }

    public override IEnumerator BulletShoot()
    {
        if (canShoot)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BlackHoleBullet bullet = bulletObj.GetComponent<BlackHoleBullet>();
            bullet.Initialize(this);

            canShoot = false;
            yield return new WaitForSeconds(shootCD);
            canShoot = true;
        }
    }

    public override void Ultimate()
    {
        GameObject bulletObj = Instantiate(bhUltBullet, transform.position, Quaternion.identity);
        BlackHoleBullet bullet = bulletObj.GetComponent<BlackHoleBullet>();
        bullet.Initialize(this);
        EnergyManager.instance.ResetEnergy();
    }

    public void BlackHoleInstantiate(Vector2 enemyPos)
    {
        GameObject blackHoleObj = Instantiate(blackHolePrefab, enemyPos, Quaternion.identity);
        BlackHole bh = blackHoleObj.GetComponent<BlackHole>();

        bh.BlackHoleDuration(blackHoleDurationTime);
        bh.Initialize(this);

        isBlackHoleSpawn = true;
    }
}
