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
        transform.localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();

        sequence.Join(sr.DOFade(1, 1f));
        sequence.Join(transform.DOScale(Vector3.one, 1f));

        sequence.OnComplete(() =>
        {
            StartCoroutine(FadeOutAndDestroy(durationTime));
        });
    }

    private IEnumerator FadeOutAndDestroy(float durationTime)
    {
        yield return new WaitForSeconds(durationTime);

        Sequence fadeOutSequence = DOTween.Sequence();

        fadeOutSequence.Join(sr.DOFade(0, 1f));
        fadeOutSequence.Join(transform.DOScale(Vector3.zero, 1f));

        fadeOutSequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, 100f * Time.deltaTime);
    }
}
