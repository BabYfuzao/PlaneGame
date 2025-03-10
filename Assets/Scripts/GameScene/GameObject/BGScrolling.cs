using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrolling : MonoBehaviour
{
    public bool canMove;
    public float speed;

    [SerializeField]
    private Renderer r;

    private void Update()
    {
        if (canMove)
        {
            r.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
        }
    }
}
