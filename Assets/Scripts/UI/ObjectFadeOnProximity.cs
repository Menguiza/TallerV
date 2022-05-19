using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class ObjectFadeOnProximity : MonoBehaviour
{
    [SerializeField] float textRange;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI dialogText;

    // Update is called once per frame
    void Update()
    {
        float deltaAlpha;
        float distanceFromPlayer = Vector3.Distance(transform.position, GameMaster.instance.playerObject.transform.position);

        if (distanceFromPlayer > textRange) deltaAlpha = -Time.deltaTime;
        else deltaAlpha = Time.deltaTime;

        deltaAlpha = (float)Math.Round(deltaAlpha, 3);
        float newAlpha = Mathf.Clamp(image.color.a + deltaAlpha, 0, 1);

        image.color = new Color(1, 1, 1, newAlpha);
        dialogText.color = new Color(0, 0, 0, newAlpha);
    }
}
