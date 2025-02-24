using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBWeapon : WeaponBase
{
    public override IEnumerator BulletShoot()
    {
        yield return base.BulletShoot();
    }
}
