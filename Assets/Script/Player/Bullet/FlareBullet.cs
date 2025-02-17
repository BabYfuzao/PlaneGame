using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareBullet : PlayerBulletBase
{
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, 1500f * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
