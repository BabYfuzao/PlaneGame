using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleWeapon : WeaponBase
{
    protected override void Start()
    {
        base.Start();
    }

    public override IEnumerator BulletShoot()
    {
        yield return base.StartCoroutine(BulletShoot());
    }
}
