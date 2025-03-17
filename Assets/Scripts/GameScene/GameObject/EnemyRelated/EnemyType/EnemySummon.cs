using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummon : MonoBehaviour
{
    public Player player;
    public float moveSpeed = 2f;

    public Animator anim;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("EnemyRemover"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
            anim.SetTrigger("isDead");
        }
    }

    public void DestroyGamObject()
    {
        Destroy(gameObject);
    }
}
