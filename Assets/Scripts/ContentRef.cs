using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRef : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Inventory.instance.content = gameObject;
    }
}
