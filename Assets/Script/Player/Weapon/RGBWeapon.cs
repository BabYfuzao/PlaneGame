using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RGBBullet;

public class RGBWeapon : WeaponBase
{
    public GameObject[] buffVFXs;

    public override IEnumerator BulletShoot()
    {
        if (canShoot)
        {
            SoundManager.instance.PlayRGBBulletShootSFX();
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            canShoot = false;
            yield return new WaitForSeconds(shootCD);
            canShoot = true;
        }
    }

    public void SetEnemyBuff(EnemyBase enemy, RGBBulletType bulletType)
    {
        enemy.rgbBuffObj.SetActive(true);

        switch (bulletType)
        {
            case RGBBulletType.Red:
                enemy.isRed = true;
                enemy.rgbBuffObj.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case RGBBulletType.Green:
                enemy.isGreen = true;
                enemy.rgbBuffObj.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case RGBBulletType.Blue:
                enemy.isBlue = true;
                enemy.rgbBuffObj.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            default:
                break;
        }

        CheckEnemyBuff(enemy);
    }

    public void CheckEnemyBuff(EnemyBase enemy)
    {
        //Palsy
        if (enemy.isRed && enemy.isGreen)
        {
            enemy.rgbBuffObj.GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(Palsy(enemy));
        }
        //Burn
        else if (enemy.isRed && enemy.isBlue)
        {
            enemy.rgbBuffObj.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0, 0.5f);
            StartCoroutine(Burn(enemy));
        }
        //Weak
        else if (enemy.isGreen && enemy.isBlue)
        {
            enemy.rgbBuffObj.GetComponent<SpriteRenderer>().color = Color.cyan;
            StartCoroutine(Weak(enemy));
        }
    }

    public IEnumerator Palsy(EnemyBase enemy)
    {
        GameObject buffVFX = Instantiate(buffVFXs[0], enemy.transform.position, Quaternion.identity);
        buffVFX.transform.SetParent(enemy.transform);

        enemy.pathFollower.canMove = false;
        enemy.isBuff = true;

        yield return new WaitForSeconds(1f);

        enemy.pathFollower.canMove = true;

        ResetBuff(enemy, buffVFX);
    }

    public IEnumerator Burn(EnemyBase enemy)
    {
        GameObject buffVFX = Instantiate(buffVFXs[1], enemy.transform.position, Quaternion.identity);
        buffVFX.transform.SetParent(enemy.transform);

        enemy.isBuff = true;
        for (int i = 0; i < 3; i++)
        {
            enemy.TakeDamage(2);
            yield return new WaitForSeconds(1f);
        }

        ResetBuff(enemy, buffVFX);
    }

    public IEnumerator Weak(EnemyBase enemy)
    {
        GameObject buffVFX = Instantiate(buffVFXs[2], enemy.transform.position, Quaternion.identity);
        buffVFX.transform.SetParent(enemy.transform);

        enemy.isBuff = true;
        enemy.isWeak = true;
        yield return new WaitForSeconds(3f);

        enemy.isWeak = false;

        ResetBuff(enemy, buffVFX);
    }

    public void ResetBuff(EnemyBase enemy, GameObject buffVFX)
    {
        enemy.isRed = false;
        enemy.isGreen = false;
        enemy.isBlue = false;

        enemy.isBuff = false;

        Destroy(buffVFX);

        enemy.rgbBuffObj.SetActive(false);
    }
}
