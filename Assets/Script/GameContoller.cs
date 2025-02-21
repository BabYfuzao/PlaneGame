using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContoller : MonoBehaviour
{
    public static GameContoller instance;

    public bool isBlackHoleSpawn;

    private void Awake()
    {
        instance = this;
    }
}
