using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothBar : MonoBehaviour
{
    public GameObject fill;
    public float maxValue;
    public float currentValue;

    public Color fullValueColor;
    public Color zeroValueColor;

    public void SetBar(int amount)
    {
        StartCoroutine(SetBarSmooth(amount));
    }

    private IEnumerator SetBarSmooth(int amount)
    {
        if (currentValue <= maxValue)
        {
            float targetValue = currentValue + amount;
            float initialValue = currentValue;
            float duration = 0.1f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                currentValue = Mathf.Lerp(initialValue, targetValue, elapsedTime / duration);
                UpdateBar();
                yield return null;
            }

            currentValue = targetValue;
            UpdateBar();
        }
    }

    public void UpdateBar()
    {
        if (currentValue >= 0)
        {
            float fillAmount = currentValue / maxValue;
            SpriteRenderer fillSpriteRenderer = fill.GetComponent<SpriteRenderer>();

            fill.transform.localScale = new Vector3(fillAmount, 1f, 1f);
            fill.transform.localPosition = new Vector3(-0.5f + (fillAmount * 0.5f), fill.transform.localPosition.y, fill.transform.localPosition.z);

            Color newColor = Color.Lerp(zeroValueColor, fullValueColor, fillAmount);
            fillSpriteRenderer.color = newColor;
        }
    }
}
