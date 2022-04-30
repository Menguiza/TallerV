using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float yRot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate((new Vector3(0f, yRot, 0f))*Time.deltaTime);   
    }
}
