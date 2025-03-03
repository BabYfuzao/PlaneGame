using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrolling : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private SpriteRenderer sr;

    private void Update()
    {
        sr.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
