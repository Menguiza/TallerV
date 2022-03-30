using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CambioPostura : MonoBehaviour
{
    [SerializeField] InicializadorSistemaPosturas.Postura stanceToChange;
    [SerializeField] InicializadorSistemaPosturas isp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            isp.Testing_AssignNewStance(stanceToChange);
        }
    }
}
