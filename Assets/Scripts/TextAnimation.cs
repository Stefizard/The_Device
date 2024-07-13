using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void IncreaseOpacity()
    {
        float increase = Time.deltaTime / duration;
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + increase);
    }

    public void ResetOpacity()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
    }
}
