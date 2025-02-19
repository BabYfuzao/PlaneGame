using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public float shootCD;
    protected bool canShoot = false;
    public GameObject bulletPrefab;

    public virtual IEnumerator BulletShoot()
    {
        if (!canShoot)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            canShoot = true;
            yield return new WaitForSeconds(shootCD);
            canShoot = false;
        }
    }
}