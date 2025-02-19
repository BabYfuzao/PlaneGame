using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float camMoveSpeed;

    void Update()
    {
        if (!EnemySpawner.instance.isBossSpawn)
        {
            transform.position += new Vector3(camMoveSpeed * Time.deltaTime, 0, 0);
        }
    }
}
