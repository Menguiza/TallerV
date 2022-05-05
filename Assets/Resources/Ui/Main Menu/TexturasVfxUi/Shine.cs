using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shine : MonoBehaviour
{
    SpriteRenderer sr;
    float alpha=0.5f;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        alpha = ((0.3f)*(Mathf.Sin(alpha)))*Time.time*(0.2f);
        print(alpha);
        sr.color = new Color(1, 1, 1, alpha);
    }
}
