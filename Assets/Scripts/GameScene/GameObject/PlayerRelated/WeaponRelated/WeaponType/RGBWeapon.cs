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
        yield return base.BulletShoot();
    }
}
