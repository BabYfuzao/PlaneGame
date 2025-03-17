using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //All Scene
    public void LoadTitleSceneButton()
    {
        SceneLoader.instance.LoadTitleScene();
    }

    //Title Scene
    public void LoadLevelSelectButton()
    {
        SceneLoader.instance.LoadLevelSelectScene();
    }

    //Level Select Scene
    public void LoadLevelSceneButton(int level)
    {
        SceneLoader.instance.LoadLevelScene(level);
    }

    //Game Scene
    public void StartButton()
    {
        GameController.instance.GameStart();
    }

    public void ReStartGameButton()
    {
        SceneLoader.instance.LoadLevelSelectScene();
    }

    public void PauseControllButton()
    {
        GameController.instance.PauseControl();
    }
}
