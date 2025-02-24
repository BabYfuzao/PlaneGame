using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void StartButton()
    {
        GameController.instance.GameStart();
    }

    public void ReStartGameButton()
    {
        SceneLoader.instance.LoadGameScene();
    }
}
