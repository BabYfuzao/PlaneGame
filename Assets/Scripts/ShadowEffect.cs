using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEffect : MonoBehaviour
{
    public List<GameObject> showPrefabs;
    public float delay;
    public int maxShadows;

    private int currentShadowIndex = 0;
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
            GameObject showObj = Instantiate(showPrefabs[currentShadowIndex], transform.position, Quaternion.identity);
            showObj.transform.localScale = new Vector3(angel, 1, 1);

            shadowQueue.Enqueue(showObj);

            if (shadowQueue.Count > maxShadows)
            {
                GameObject oldestShadow = shadowQueue.Dequeue();
                Destroy(oldestShadow);
            }

            currentShadowIndex = (currentShadowIndex + 1) % showPrefabs.Count;

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