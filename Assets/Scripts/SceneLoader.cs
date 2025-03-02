using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private void Awake()
    {
        instance = this;
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevelSelectScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevelScene(int level)
    {
        SceneManager.LoadScene(level + 1);
    }
}
