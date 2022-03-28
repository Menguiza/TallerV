using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosturaDelSueño : MonoBehaviour
{
    [Header("Sleep technique modifier")]
    [SerializeField] string ST_name;
    [SerializeField] sbyte ST_multVidaMax;
    [SerializeField] sbyte ST_multDmg;
    [SerializeField] float ST_multConciencia;
    [SerializeField] sbyte ST_multTGPC;
    [SerializeField] sbyte ST_multCritProb;
    [SerializeField] float ST_multCrit;
    [SerializeField] sbyte ST_multRoboPer;
    [SerializeField] float ST_multVelAatque;
    [SerializeField] float ST_multSpeed;

    [Header("Dream technique modifier")]
    [SerializeField] string DT_name;
    [SerializeField] sbyte DT_multVidaMax;
    [SerializeField] sbyte DT_multDmg;
    [SerializeField] float DT_multConciencia;
    [SerializeField] sbyte DT_multTGPC;
    [SerializeField] sbyte DT_multCritProb;
    [SerializeField] float DT_multCrit;
    [SerializeField] sbyte DT_multRoboPer;
    [SerializeField] float DT_multVelAatque;
    [SerializeField] float DT_multSpeed;


    void AssertGameMasterReference()
    {
        if (TryGetComponent<GameMaster>(out GameMaster gameMaster) == false)
        {
            Debug.LogWarning("|POSTURA DEL SUEÑO| No se logró acceder al GameMaster");
            return;
        }
    }

    public void Apply_SleepTechnique()
    {
        AssertGameMasterReference();
    }

    public void Remove_SleepTechnique()
    {
        AssertGameMasterReference();
    }

    public void Apply_DreamTechnique()
    {
        AssertGameMasterReference();
    }

    public void Remove_DreamTechnique()
    {
        AssertGameMasterReference();
    }
}
