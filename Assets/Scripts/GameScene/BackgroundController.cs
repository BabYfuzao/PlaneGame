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
            bg.canMove = false;
        }
    }
}
