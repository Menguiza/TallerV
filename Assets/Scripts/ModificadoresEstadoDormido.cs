using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificadoresEstadoDormido : MonoBehaviour
{
    //Modificar para el estado pesadilla, falta agregar

    public void AddDreamModifier()
    {
        GameMaster.instance.AddMod("Arte del soñador", 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0.5f);
    }

    public void RemoveDreamModifier()
    {
        GameMaster.instance.RemoveMod("Arte del soñador");
    }
}
