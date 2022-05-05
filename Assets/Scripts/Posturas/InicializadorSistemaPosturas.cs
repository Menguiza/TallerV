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
    GameMaster gm;
    public List<PosturaDelSueño> posturasDelSueño;


    private void Awake()
    {
        
        if (GameObject.Find("GameMaster").TryGetComponent<GameMaster>(out GameMaster gameMaster) == false)
        {
            Debug.LogWarning("|Sistema de posturas| No se logró acceder al GameMaster");
            return;
        }
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        //Inicializa la postura
        gm.posturaDelSueño = posturasDelSueño[(int)gm.IDPostura];
        gm.RemoveActiveTechniques();
        gm.ApplyTechniques();
    }

    public void AssignNewStance(Postura postura)
    {
        gm.IDPostura = postura;
        gm.RemoveActiveTechniques();
        gm.posturaDelSueño = posturasDelSueño[(int)postura];
        gm.ApplyTechniques();
    }
}
