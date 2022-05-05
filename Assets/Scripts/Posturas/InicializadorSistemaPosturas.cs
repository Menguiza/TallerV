using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Postura
{
    Base = 0,
    Da�o = 1,
    Proteccion = 2,
    Critico = 3,
    Recarga = 4,
    Pesadilla = 5
}

public class InicializadorSistemaPosturas : MonoBehaviour
{
    public List<PosturaDelSue�o> posturasDelSue�o;


    private void Start()
    {
        
        if (GameObject.Find("GameMaster").TryGetComponent<GameMaster>(out GameMaster gameMaster) == false)
        {
            Debug.LogWarning("|Sistema de posturas| No se logr� acceder al GameMaster");
            return;
        }

        //Inicializa la postura
        GameMaster.instance.posturaDelSue�o = posturasDelSue�o[(int)GameMaster.instance.IDPostura];
        GameMaster.instance.RemoveActiveTechniques();
        GameMaster.instance.ApplyTechniques();
    }

    public void AssignNewStance(Postura postura)
    {
        GameMaster.instance.IDPostura = postura;
        GameMaster.instance.RemoveActiveTechniques();
        GameMaster.instance.posturaDelSue�o = posturasDelSue�o[(int)postura];
        GameMaster.instance.ApplyTechniques();
    }
}
