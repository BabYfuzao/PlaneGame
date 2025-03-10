using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("-Compponent-")]
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator animator;

    public WeaponBase weapon;
    public SmoothBar hPBar;

    [Header("-Player status-")]
    public int hPMax;
    public int hP;
    public float moveSpeed;

    public Color defaultColor;

    [Header("-Movement Area-")]
    public Vector2 moveAreaSize = new Vector2(16f, 9f);

    private void Start()
    {
        hP = hPMax;
        hPBar.maxValue = hPMax;
        hPBar.currentValue = hPBar.maxValue;
        hPBar.SetBar(hP);

        defaultColor = sr.color;
    }
    void Update()
    {
        PlayerMove();
        PlayerAttack();
    }

    public void PlayerMove()
    {
        //Player move
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = move * moveSpeed;

        Vector2 newPosition = transform.localPosition;
        float minX = -moveAreaSize.x / 2f;
        float maxX = moveAreaSize.x / 2f;
        float minY = -moveAreaSize.y / 2f;
        float maxY = moveAreaSize.y / 2f;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.localPosition = newPosition;

        bool isUp = (move.x > 0 && move.y == 0) ||  move.y > 0;
        bool isDown = move.y < 0;

        animator.SetBool("isUp", isUp);
        animator.SetBool("isDown", isDown);
    }

    public void PlayerAttack()
    {
        //Bullet shoot
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(weapon.BulletShoot());
        }

        //Ult
        if (Input.GetKey(KeyCode.J))
        {
            if (EnergyManager.instance.canUlt)
            {
                weapon.Ultimate();
                EnergyManager.instance.ResetEnergy();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        EnergyManager.instance.ReloadEnergy(weapon, 1);
        hP -= damage;
        hPBar.SetBar(hP);
        StartCoroutine(HitEffect());
        CheckDead();
    }

    public IEnumerator HitEffect()
    {
        Color currentColor = sr.color;

        Color darkerColor = currentColor * 0.5f;
        sr.color = darkerColor;

        yield return new WaitForSeconds(0.2f);
        sr.color = defaultColor;
    }

    public void CheckDead()
    {
        if (hP <= 0)
        {
            animator.SetBool("isDead", true);

        }
    }

    public void PlayerDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(0f, 0f), new Vector2(moveAreaSize.x, moveAreaSize.y));
    }
}
