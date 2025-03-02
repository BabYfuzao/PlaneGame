using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RGBBullet;

public class RGBBuff : MonoBehaviour
{
    private EnemyBase enemy;

    public SpriteRenderer sr;
    public bool isRed, isGreen, isBlue;
    public bool isBuff;

    public GameObject[] buffVFXs;

    public void Initialize(EnemyBase enemyInstance)
    {
        enemy = enemyInstance;
    }

    public void SetBuff(RGBBulletType bulletType)
    {
        if (!isBuff)
        {
            switch (bulletType)
            {
                case RGBBulletType.Red:
                    isRed = true;
                    sr.color = Color.red;
                    break;
                case RGBBulletType.Green:
                    isGreen = true;
                    sr.color = Color.green;
                    break;
                case RGBBulletType.Blue:
                    isBlue = true;
                    sr.color = Color.blue;
                    break;
                default:
                    break;
            }

            CheckBuff();
        }
    }

    public void CheckBuff()
    {
        //Palsy
        if (isRed && isGreen)
        {
            isBuff = true;
            sr.color = Color.yellow;
            StartCoroutine(Palsy());
        }
        //Burn
        else if (isRed && isBlue)
        {
            isBuff = true;
            sr.color = new Color(0.5f, 0, 0.5f);
            StartCoroutine(Burn());
        }
        //Weak
        else if (isGreen && isBlue)
        {
            isBuff = true;
            sr.color = Color.cyan;
            StartCoroutine(Weak());
        }
    }

    public IEnumerator Palsy()
    {
        GameObject buffVFX = Instantiate(buffVFXs[0], enemy.transform.position, Quaternion.identity);
        buffVFX.transform.SetParent(enemy.transform);

        enemy.canMove = false;

        yield return new WaitForSeconds(1.5f);

        enemy.canMove = true;

        DestroyBuff(buffVFX);
    }

    public IEnumerator Burn()
    {
        GameObject buffVFX = Instantiate(buffVFXs[1], enemy.transform.position, Quaternion.identity);
        buffVFX.transform.SetParent(enemy.transform);

        for (int i = 0; i < 3; i++)
        {
            enemy.TakeDamage(2);
            yield return new WaitForSeconds(1f);
        }

        DestroyBuff(buffVFX);
    }

    public IEnumerator Weak()
    {
        GameObject buffVFX = Instantiate(buffVFXs[2], enemy.transform.position, Quaternion.identity);
        buffVFX.transform.SetParent(enemy.transform);

        enemy.isWeak = true;
        yield return new WaitForSeconds(3f);

        enemy.isWeak = false;

        DestroyBuff(buffVFX);
    }

    public void DestroyBuff(GameObject buffVFX)
    {
        Destroy(buffVFX);
        Destroy(gameObject);
    }
}
