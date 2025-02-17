using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlackHole : MonoBehaviour
{
    public SpriteRenderer sr;
    public void BlackHoleDuration(float durationTime)
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);

        sr.DOFade(1, 1f).OnComplete(() =>
        {
            StartCoroutine(FadeOutAndDestroy(durationTime));
        });
    }

    private IEnumerator FadeOutAndDestroy(float durationTime)
    {
        yield return new WaitForSeconds(durationTime);

        sr.DOFade(0, 1f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, 100f * Time.deltaTime);
    }
}
