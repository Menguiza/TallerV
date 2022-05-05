using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CambioPostura : MonoBehaviour
{
    [SerializeField] Postura stanceToChange;
    [SerializeField] InicializadorSistemaPosturas isp;

    private void Start()
    {
        isp = FindObjectOfType<InicializadorSistemaPosturas>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            isp.AssignNewStance(stanceToChange);
        }
    }
}
