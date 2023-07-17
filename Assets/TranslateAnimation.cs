using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateAnimation : MonoBehaviour
{
    public void Play(Vector3 destination, float duration)
    {
        StartCoroutine(TranslateCoroutine(destination, duration));
    }
    IEnumerator TranslateCoroutine(Vector3 dest, float duration)
    {
        Vector3 start = transform.position;
        Vector3 direction = dest - start;
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            transform.position = start + t / duration * direction;
            yield return null;
        }
        transform.position = dest;
    }
}
