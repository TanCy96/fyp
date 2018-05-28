using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudEffect : MonoBehaviour
{
    Color myColor;
    float fadeSpeed;

    void Start()
    {
        fadeSpeed = 2f;
        myColor = new Color32(255, 255, 255, 255);
        GetComponent<Image>().color = myColor;
    }

    void OnEnable()
    {
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        float value = 1f;

        while (value > 0f)
        {
            value -= fadeSpeed * Time.deltaTime;
            GetComponent<Image>().color = new Color(myColor.r, myColor.g, myColor.b, value);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
