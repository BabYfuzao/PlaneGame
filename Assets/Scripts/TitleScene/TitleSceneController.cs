using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneController : MonoBehaviour
{
    public static TitleSceneController instance;

    [Header("-Level Select Control-")]
    public GameObject levelSelectButton;
    public GameObject levelSelectPanel;
    private bool canLSBDisplay = true;
    private bool canLSPDisplay = false;

    private void Awake()
    {
        instance = this;
    }

    public void LevelSelectPanel()
    {
        canLSBDisplay = !canLSBDisplay;
        canLSPDisplay = !canLSPDisplay;

        levelSelectButton.SetActive(canLSBDisplay);
        levelSelectPanel.SetActive(canLSPDisplay);
    }
}
