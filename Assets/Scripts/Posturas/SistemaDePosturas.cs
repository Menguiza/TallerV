using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDePosturas : MonoBehaviour
{
    GameMaster gm;
    public List<PosturaDelSueño> posturasDelSueño;
    enum Postura
    {
        Daño = 0,
        Proteccion = 1,
        ProbCritica = 2,
        RecargaRapida = 3,
    }

    private void Start()
    {
        if (TryGetComponent<GameMaster>(out GameMaster gameMaster) == false)
        {
            Debug.LogWarning("|Sistema de posturas| No se logró acceder al GameMaster");
        }
        gm = GetComponent<GameMaster>();
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Apply_SleepTechnique();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Remove_SleepTechnique();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Apply_DreamTechnique();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Remove_DreamTechnique();
        }
    }

    void AssertGameMasterReference()
    {
        if (transform.parent.gameObject.TryGetComponent<GameMaster>(out GameMaster gameMaster) == false)
        {
            Debug.LogWarning("|POSTURA DEL SUEÑO| No se logró acceder al GameMaster");
            return;
        }
    }

    public void Apply_SleepTechnique()
    {
        AssertGameMasterReference();
        GetComponentInParent<GameMaster>().AddMod(ST_name, ST_multVidaMax, ST_multDmg, ST_multConciencia, ST_multTGPC, ST_multCritProb, ST_multCrit, ST_multRoboPer, ST_multVelAatque, ST_multSpeed, ST_multPesadillaPer);
    }

    public void Remove_SleepTechnique()
    {
        AssertGameMasterReference();
        foreach (Mods mod in GetComponentInParent<GameMaster>().mods)
        {
            if (mod.Name == ST_name)
            {
                GetComponentInParent<GameMaster>().mods.Remove(mod);
                GetComponentInParent<GameMaster>().CheckMods();
                return;
            }
        }
        Debug.LogWarning("|POSTURA DEL SUEÑO| No se encontró el Modificador de la Técnica de dormir " + ST_name + " a remover");
    }

    public void Apply_DreamTechnique()
    {
        AssertGameMasterReference();
        GetComponentInParent<GameMaster>().AddMod(DT_name, DT_multVidaMax, DT_multDmg, DT_multConciencia, DT_multTGPC, DT_multCritProb, DT_multCrit, DT_multRoboPer, DT_multVelAatque, DT_multSpeed, DT_multPesadillaPer);
    }

    public void Remove_DreamTechnique()
    {
        AssertGameMasterReference();
        foreach (Mods mod in GetComponentInParent<GameMaster>().mods)
        {
            if (mod.Name == DT_name)
            {
                GetComponentInParent<GameMaster>().mods.Remove(mod);
                GetComponentInParent<GameMaster>().CheckMods();
                return;
            }
        }
        Debug.LogWarning("|POSTURA DEL SUEÑO| No se encontró el Modificador de la Técnica del sueño " + DT_name + " a remover");
    }*/

}
