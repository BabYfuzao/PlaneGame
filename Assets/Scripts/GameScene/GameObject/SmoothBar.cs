using System.Collections;
using UnityEngine;

public class SmoothBar : MonoBehaviour
{
    public GameObject fill;
    public float maxValue;
    public float currentValue;

    public Color fullValueColor;
    public Color zeroValueColor;

    public void SetBar(float targetAmount)
    {
        StartCoroutine(SetBarSmooth(targetAmount));
    }

    private IEnumerator SetBarSmooth(float targetAmount)
    {
        if (maxValue > 0)
        {
            float duration = 0.1f;
            float elapsedTime = 0f;

            float startingValue = currentValue;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                currentValue = Mathf.Lerp(startingValue, targetAmount, elapsedTime / duration);
                UpdateBar();
                yield return null;
            }

            currentValue = targetAmount;
            UpdateBar();
        }
    }

    public void UpdateBar()
    {
        if (currentValue >= 0 && currentValue <= maxValue)
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