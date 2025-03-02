using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitCount : MonoBehaviour
{
    public TextMeshPro hitCountText;
    public int hitCount = 0;

    public void HitCountUpdate(int amount)
    {
        hitCount += amount;

        if (hitCount <= 0)
        {
            Destroy(gameObject);
            return;
        }

        TextHandle();
    }

    public void TextHandle()
    {
        hitCountText.text = hitCount.ToString();
    }
}
