using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToxicFog : EnemyNormalBullet
{
    public bool canDamage;

    protected override void Start()
    {
        base.Start();
    }

    public void ScaleChange()
    {
        StartCoroutine(StartScaleChange());
    }

    public IEnumerator StartScaleChange()
    {
        while (true)
        {
            transform.localScale = new Vector3(0.98f, 0.98f, 0.98f);
            yield return new WaitForSeconds(0.2f);

            transform.Rotate(0, 0, 2f);

            transform.localScale = new Vector3(1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyRemover"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            canDamage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            canDamage = false;
            StopCoroutine(StartDamage(player));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            StartCoroutine(StartDamage(player));
        }
    }

    public IEnumerator StartDamage(Player player)
    {
        if (canDamage)
        {
            canDamage = false;
            player.TakeDamage(atk);
            yield return new WaitForSeconds(1f);
            canDamage = true;
        }
    }
}
