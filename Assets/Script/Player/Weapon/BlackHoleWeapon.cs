using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleWeapon : WeaponBase
{
    public override IEnumerator BulletShoot()
    {
        yield return base.BulletShoot();
    }
}
