 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [Header("-Object-")]
    public SmoothBar energyBar;

    public GameObject bulletPrefab;

    [Header("-Data-")]
    public int energyMax;
    public float shootCD;

    [Header("-Status-")]
    protected bool canShoot = true;

    [Header("-Other-")]
    public int weaponID;

    protected virtual void Start()
    {
        EnergyManager.instance.SetDefault(energyMax);
    }

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

    public virtual void Ultimate()
    {

    }
}