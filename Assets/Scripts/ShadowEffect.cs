using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEffect : MonoBehaviour
{
    public GameObject shadowPrefab;
    public float delay;
    public int maxShadows;

    public Queue<GameObject> shadowQueue = new Queue<GameObject>();
    public bool canGen = true;

    public void StartShadowEffect(float angel)
    {
        canGen = true;
        StartCoroutine(GenerateShadowEffect(angel));
    }

    private IEnumerator GenerateShadowEffect(float angel)
    {
        while (canGen)
        {
            GameObject shadowObj = Instantiate(shadowPrefab, transform.position, Quaternion.identity);
            SpriteRenderer shadowSR = shadowObj.GetComponent<SpriteRenderer>();
            shadowSR.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;

            shadowObj.transform.localScale = new Vector3(angel, 1, 1);

            shadowQueue.Enqueue(shadowObj);

            if (shadowQueue.Count > maxShadows)
            {
                GameObject oldestShadow = shadowQueue.Dequeue();
                Destroy(oldestShadow);
            }

            yield return new WaitForSeconds(delay);
        }
    }

    public IEnumerator DestroyAllShadows()
    {
        while (shadowQueue.Count > 0)
        {
            GameObject shadow = shadowQueue.Dequeue();
            Destroy(shadow);

            yield return new WaitForSeconds(delay);
        }
    }

    public void StopShadowEffect()
    {
        canGen = false;
        StartCoroutine(DestroyAllShadows());
    }
}