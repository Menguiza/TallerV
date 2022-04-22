using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMatColor : MonoBehaviour
{
    //CAMILO ESTUPIDO

    Material mat;
    public Color color;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        mat.SetColor("_EmissionColor", color);
    }
}
