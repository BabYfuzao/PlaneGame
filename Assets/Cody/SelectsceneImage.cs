using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using DG.Tweening;

public class SelectsceneImage : MonoBehaviour
{
    public GameObject imageGroup;
    // Start is called before the first frame update
    void Start()
    {
        delayImage();
    }

    void delayImage()
    {
        CanvasGroup canvasGroup = imageGroup.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 2f);
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
