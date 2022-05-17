using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificadoresEstadoDormido : MonoBehaviour
{
    //Modificar para el estado pesadilla, falta agregar

    public void AddDreamModifier()
    {
        StartCoroutine(ApplyDreamModifierOnNextFrame());
    }


    public void RemoveDreamModifier()
    {
        GameMaster.instance.RemoveMod("Arte del soñador");
        GameMaster.instance.RemoveMod("Caos de pesadilla");
    }

    IEnumerator ApplyDreamModifierOnNextFrame()
    {
        yield return null;

        print(GameMaster.instance.Player.Pesadilla + " pre");
        if (GameMaster.instance.Player.Pesadilla) GameMaster.instance.AddMod("Caos de pesadilla", 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 2f, 0);
        else GameMaster.instance.AddMod("Arte del soñador", 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 1f, 0);
        print(GameMaster.instance.Player.Pesadilla + " post");
        StopCoroutine(ApplyDreamModifierOnNextFrame());
    }
}
