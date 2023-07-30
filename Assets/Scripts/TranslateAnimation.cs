using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateAnimation : MonoBehaviour
{
    [SerializeField] GameObject destination;
    [SerializeField] float duration = 2f;

    public void Play()
    {
        StartCoroutine(TranslateCoroutine());
    }
    IEnumerator TranslateCoroutine()
    {
        Vector3 start = transform.position;
        Vector3 dest = destination.transform.position;
        Vector3 direction = dest - start;
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            transform.position = start + t / duration * direction;
            yield return null;
        }
        transform.position = dest;
    }
}
