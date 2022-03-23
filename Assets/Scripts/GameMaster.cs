using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameMaster : MonoBehaviour
{
    //Singleton
    GameMaster instance;

    [Header("Valores Iniciales (Modificables)")]

    [SerializeField]
    private uint maxLife;
    [SerializeField]
    private uint dmg;
    [SerializeField]
    private float multiplicadorConciencia = 1.0f;

    //Referencias Al Jugador
    Player player;
    public GameObject playerObject;

    //Modificadores
    public List<Mods> mods = new List<Mods>();

    #region"Inputs Editor"
    //Valores Custom Inspector
    [HideInInspector]
    public string nameINP;
    [HideInInspector]
    public byte vidaINP, dmgINP, tgpcINP, critProbINP, roboDeVidaINP;
    [HideInInspector]
    public float multConcienciaINP, critMultINP, multVelAtaqueINP, speedMultINP;
    [HideInInspector]
    public int damageToPlayer;
    #endregion

    //Variables de utilidad
    byte porcentual = 10;
    byte hundred = 100;
    byte one = 1;
    float suma = 0f;
    byte zero = 0;
    byte multTGPC = 1;

    [Header("TGPC")]

    public float timeToReset = 1f;
    [HideInInspector]
    public bool enableTGPC = true;

    //Estados del jugador
    public enum estado {Despierto, Dormido};
    
    //Accesor Clase "Player"
    public Player Player { get => player;}

    //Accesor Multiplicador de Conciencia
    internal float MultiplicadorConciencia 
    { 
        get => multiplicadorConciencia;
        set
        {
            value = (float)Math.Round(value, 1);

            if(value > 0)
            {
                multiplicadorConciencia = value;
            }
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        player = new(maxLife,dmg);

        CheckMods();
    }

    private void Update()
    {
        Inconciencia();
    }

    #region"Sistema de modificadores y Estadisticas"

    public void AddMod(string name, uint vida, uint dmg, float multConciencia, uint multTGPC, byte critProb, float critMult,
        byte multRoboPer, float multRobo, float multSpeed)
    {

        if(mods.Count != 0)
        {
            foreach (Mods element in mods)
            {
                if (element.Name == name)
                {
                    return;
                }
            }

            mods.Add(new Mods(name, (byte)vida, (byte)dmg, multConciencia, (byte)multTGPC, critProb, critMult, multRoboPer, multRobo, multSpeed));

            CheckMods();
        }
        else if(mods.Count == 0)
        {
            mods.Add(new Mods(name, (byte)vida, (byte)dmg, multConciencia, (byte)multTGPC, critProb, critMult, multRoboPer, multRobo, multSpeed));

            CheckMods();
        }
    }

    void CheckMods()
    {
        if(mods.Count !=0)
        {
            foreach (Mods element in mods)
            {
                if(element.Utilizado == false)
                {
                    uint maxOld = player.MaxLife;
                    player.Damage = player.Damage + (uint)Mathf.Round((dmg * (element.MultDmg * porcentual)) / hundred);
                    player.MaxLife = player.MaxLife + (uint)Mathf.Round((maxLife * (element.MultVidaMax * porcentual)) / hundred);
                    MultiplicadorConciencia += element.MultConciencia;
                    player.TGPC += element.MultTGPC;
                    player.CritProb += element.MultCritProb;
                    player.CritMult += element.MultCrit;
                    player.RoboVida += element.MultRoboPer;
                    player.MultVelAtaque += element.MultVelAtaque;
                    player.SpeedMult += element.MultSpeed;
                    element.Utilizado = true;
                    CorrectLife(maxOld, player.MaxLife);
                }
            }
        }
    }

    public void ResetStats()
    {
        uint maxOld = player.MaxLife;
        player.MaxLife = maxLife;
        player.Damage = dmg;
        MultiplicadorConciencia = one;
        player.TGPC = one;
        player.CritProb = zero;
        player.CritMult = one;
        player.RoboVida = zero;
        player.MultVelAtaque = one;
        player.SpeedMult = one;
        CorrectLife(maxOld, player.MaxLife);
    }

    public void ResetStats(Mods mod)
    {
        uint maxOld = player.MaxLife;
        player.MaxLife -= (uint)Mathf.Round((maxLife * (mod.MultVidaMax * porcentual)) / 100);
        player.Damage -= (uint)Mathf.Round((dmg * (mod.MultDmg * porcentual)) / hundred); ;
        CorrectLife(maxOld, player.MaxLife);
        player.TGPC -= mod.MultTGPC;
        MultiplicadorConciencia -= mod.MultConciencia;
        player.CritProb -= mod.MultCritProb;
        player.CritMult -= mod.MultCrit;
        player.RoboVida -= mod.MultRoboPer;
        player.MultVelAtaque -= mod.MultVelAtaque;
        player.SpeedMult -= mod.MultSpeed;
        mods.Remove(mod);
    }

    private void ResetUseMods()
    {
        foreach(Mods element in mods)
        {
            element.Utilizado = false;
        }
    }

    void CorrectLife(uint maxLife, uint newMaxLife)
    {
        float result = maxLife - player.Life;

        if (player.Life >= newMaxLife || newMaxLife > maxLife)
        {
            player.Life = newMaxLife;
        }
    }

    #endregion

    #region"Acciones del Jugador"

    #endregion

    #region"Acciones hacia el jugador"

    public void DamagePlayer(int value)
    {
        int opVida = (int)(player.Life - value);
        player.Life = (uint)Mathf.Max(0, opVida);

        int opConci = (int)(player.Conciencia - (value * multiplicadorConciencia));
        player.Conciencia = (ushort)Mathf.Max(0,opConci);
    }

    void Inconciencia()
    {
        if(enableTGPC == true && player.Status == estado.Dormido)
        {
            enableTGPC = false;
            suma = zero;
            multTGPC = one;
            StartCoroutine(MultTGPC());
        }
    }

    IEnumerator MultTGPC()
    {
        yield return new WaitForSeconds(one);

        while (player.Conciencia < player.MaxConciencia)
        {
            suma = player.TGPC * multTGPC;
            player.Conciencia += (ushort)suma;
            multTGPC += one;

            yield return new WaitForSeconds(one);
        }

        enableTGPC = true;
    }

    #endregion
}