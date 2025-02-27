using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //Title Scene
    public void LevelSelectPanel()
    {
        TitleSceneController.instance.LevelSelectPanel();
    }

    //Game Scene
    public void StartButton()
    {
        GameController.instance.GameStart();
    }

    public void ReStartGameButton()
    {
        //SceneLoader.instance.LoadGameScene();
    }
}
