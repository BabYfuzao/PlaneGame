using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public static BackgroundController instance;

    public BGScrolling[] bgs;

    private void Awake()
    {
        instance = this;
    }

    public void BGStop()
    {
        foreach (var bg in bgs)
        {
            StartCoroutine(BGSpeedReduce(bg));
        }
    }

    public IEnumerator BGSpeedReduce(BGScrolling bg)
    {
        while (bg.speed > 0f)
        {
            bg.speed -= Time.deltaTime * 0.1f;
            if (bg.speed <= 0f)
            {
                bg.speed = 0f;
                bg.canMove = false;
            }
            yield return null;
        }
    }
}
