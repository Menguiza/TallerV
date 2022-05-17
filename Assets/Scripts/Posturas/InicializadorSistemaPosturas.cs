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
        
        if (GameMaster.instance.sleepParticle.transform.childCount > 0)
        {
            Destroy(GameMaster.instance.sleepParticle.transform.GetChild(0).gameObject);
        }

        print(postura);

        switch (postura)
        {
            case Postura.Base:
                GameMaster.instance.sleepParticlePrefab = (GameObject)Resources.Load("Prefabs/Posturas/F_Sleep_Normal");
                break;
            case Postura.Da�o:
                GameMaster.instance.sleepParticlePrefab = (GameObject)Resources.Load("Prefabs/Posturas/F_Sleep_Power");
                break;
            case Postura.Proteccion:
                GameMaster.instance.sleepParticlePrefab = (GameObject)Resources.Load("Prefabs/Posturas/F_Sleep_Protection");
                break;
            case Postura.Critico:
                GameMaster.instance.sleepParticlePrefab = (GameObject)Resources.Load("Prefabs/Posturas/F_Sleep_Crit");
                break;
            case Postura.Recarga:
                GameMaster.instance.sleepParticlePrefab = (GameObject)Resources.Load("Prefabs/Posturas/F_Sleep_FR");
                break;
            case Postura.Pesadilla:
                GameMaster.instance.sleepParticlePrefab = (GameObject)Resources.Load("Prefabs/Posturas/F_Sleep_Nightmare");
                break;
        }

        Instantiate(GameMaster.instance.sleepParticlePrefab, GameMaster.instance.sleepParticle.transform.position, Quaternion.identity, GameMaster.instance.sleepParticle.transform);
    }
}