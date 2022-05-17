using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FadeScreen : MonoBehaviour
{
    Image fadePanel;
    [SerializeField] float fadeTime;

    void Start()
    {
        fadePanel = GetComponent<Image>();
        fadePanel.color = Color.black;

        StartCoroutine(FadeWhite());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) StartCoroutine(FadeWhite());
        if (Input.GetKeyDown(KeyCode.K)) StartCoroutine(FadeBlack());
    }

    IEnumerator FadeWhite()
    {
        StopCoroutine(FadeBlack());

        while (fadePanel.color.a != 0)
        {
            float newAlpha = (float)Math.Round(fadePanel.color.a - Time.deltaTime/fadeTime, 3);
            if (newAlpha < 0) newAlpha = 0;

            fadePanel.color = new Color(0, 0, 0, newAlpha);

            yield return null;
        }

        StopCoroutine(FadeWhite());
    }

    IEnumerator FadeBlack()
    {
        StopCoroutine(FadeWhite());

        while (fadePanel.color.a != 1)
        {
            float newAlpha = (float)Math.Round(fadePanel.color.a + Time.deltaTime/fadeTime, 3);
            if (newAlpha > 1) newAlpha = 1;

            fadePanel.color = new Color(0, 0, 0, newAlpha);

            yield return null;
        }

        StopCoroutine(FadeBlack());
    }
}
