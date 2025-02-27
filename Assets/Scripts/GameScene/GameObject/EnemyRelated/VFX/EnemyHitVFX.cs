using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitVFX : MonoBehaviour
{
    private void Start()
    {
        Invoke("Destroy", 0.5f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
