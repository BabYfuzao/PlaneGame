using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalWeapon : WeaponBase
{
    public GameObject hitCountPrefab;

    public GameObject explosionPrefab;

    private List<GameObject> hitEnemies = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
    }

    public override IEnumerator BulletShoot()
    {
        if (canShoot)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            DigitalBullet bullet = bulletObj.GetComponent<DigitalBullet>();
            bullet.Initialize(this);

            canShoot = false;
            yield return new WaitForSeconds(shootCD);
            canShoot = true;
        }
    }

    public override void Ultimate()
    {
        hitEnemies.RemoveAll(enemy => enemy == null);

        if (hitEnemies.Count > 0)
        {
            foreach (GameObject enemy in hitEnemies)
            {
                GameObject explosionObj = Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
                Explosion explosion = explosionObj.GetComponent<Explosion>();
                explosionObj.GetComponent<Explosion>().StartExplosion();
            }
            hitEnemies.Clear();
        }
        EnergyManager.instance.ResetEnergy();
    }

    public void RecordHitEnemy(GameObject enemy)
    {
        if (!hitEnemies.Contains(enemy))
        {
            hitEnemies.Add(enemy);
        }
    }
}
