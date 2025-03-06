using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalWeapon : WeaponBase
{
    public GameObject hitCountPrefab;

    protected override void Start()
    {
        base.Start();
    }

    public override IEnumerator BulletShoot()
    {
        yield return base.BulletShoot();
    }
}
