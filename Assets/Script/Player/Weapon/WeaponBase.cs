 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public float shootCD;
    protected bool canShoot = true;
    public GameObject bulletPrefab;

    public int weaponID;

    public virtual IEnumerator BulletShoot()
    {
        if (canShoot)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            canShoot = false;
            yield return new WaitForSeconds(shootCD);
            canShoot = true;
        }
    }
}