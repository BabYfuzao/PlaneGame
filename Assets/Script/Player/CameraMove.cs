using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove instance;

    public bool canMove;
    public float camMoveSpeed;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (canMove)
        {
            transform.position += new Vector3(camMoveSpeed * Time.deltaTime, 0, 0);
        }
    }
}
