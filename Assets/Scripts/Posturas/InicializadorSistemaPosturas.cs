using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Postura
{
    Base = 0,
    Daño = 1,
    Proteccion = 2,
    Critico = 3,
    Recarga = 4,
    Pesadilla = 5
}

public class InicializadorSistemaPosturas : MonoBehaviour
{
    public List<PosturaDelSueño> posturasDelSueño;


    private void Start()
    {
        
        if (GameObject.Find("GameMaster").TryGetComponent<GameMaster>(out GameMaster gameMaster) == false)
        {
            Debug.LogWarning("|Sistema de posturas| No se logró acceder al GameMaster");
            return;
        }

        //Inicializa la postura
        GameMaster.instance.posturaDelSueño = posturasDelSueño[(int)GameMaster.instance.IDPostura];
        GameMaster.instance.RemoveActiveTechniques();
        GameMaster.instance.ApplyTechniques();
    }

    public void AssignNewStance(Postura postura)
    {
        GameMaster.instance.IDPostura = postura;
        GameMaster.instance.RemoveActiveTechniques();
        GameMaster.instance.posturaDelSueño = posturasDelSueño[(int)postura];
        GameMaster.instance.ApplyTechniques();
    }
}
