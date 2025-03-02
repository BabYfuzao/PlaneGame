using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneController : MonoBehaviour
{
    public static TitleSceneController instance;

    private void Awake()
    {
        instance = this;
    }
}
