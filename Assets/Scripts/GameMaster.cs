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
    public sbyte vidaINP, dmgINP, tgpcINP, critProbINP, roboDeVidaINP;
    [HideInInspector]
    public float multConcienciaINP, critMultINP, multVelAtaqueINP, speedMultINP;
    [HideInInspector]
    public int damageToPlayer;
    #endregion

    //Variables de utilidad
    byte hundred = 100, one = 1, porcentual = 10, zero = 0, fifty = 50, contador = 0, multTGPC = 1, three = 3;
    float suma = 0f, minConciencia = 0.1f;

    [Header("TGPC")]

    public float timeToReset = 1f;

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
            value = (float)Math.Round(value, one);

            if(value > zero)
            {
                multiplicadorConciencia = value;
            }
            else 
            {
                multiplicadorConciencia = minConciencia;
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

    private void FixedUpdate()
    {
        Inconciencia();
    }

    #region"Sistema de modificadores y Estadisticas"

    public void AddMod(string name, sbyte vida, sbyte dmg, float multConciencia, sbyte multTGPC, sbyte critProb, float critMult,
        sbyte multRoboPer, float multRobo, float multSpeed)
    {

        if(mods.Count != zero)
        {
            foreach (Mods element in mods)
            {
                if (element.Name == name)
                {
                    return;
                }
            }

            mods.Add(new Mods(name, (sbyte)vida, (sbyte)dmg, multConciencia, (sbyte)multTGPC, critProb, critMult, multRoboPer, multRobo, multSpeed));

            CheckMods();
        }
        else if(mods.Count == zero)
        {
            mods.Add(new Mods(name, (sbyte)vida, (sbyte)dmg, multConciencia, (sbyte)multTGPC, critProb, critMult, multRoboPer, multRobo, multSpeed));

            CheckMods();
        }
    }

    public void CheckMods()
    {
        float maxLifeResult = maxLife;
        float dmgResult = dmg;
        float multConcienciaResult = one;
        float tgpcResult = one;
        float critProbResult = zero;
        float critMultResult = three;
        float roboVidaResult = zero;
        float multVelAtaque = one;
        float speedMultResult = one;

        uint maxOld = player.MaxLife;

        if(mods.Count != zero)
        {
            foreach (Mods element in mods)
            {
                maxLifeResult += Mathf.Round((maxLife * (element.MultVidaMax * porcentual)) / hundred);

                dmgResult += Mathf.Round((dmg * (element.MultDmg * porcentual)) / hundred);

                multConcienciaResult += element.MultConciencia;

                tgpcResult += element.MultTGPC;

                critProbResult += element.MultCritProb;

                critMultResult += element.MultCrit;

                roboVidaResult += element.MultRoboPer;

                multVelAtaque += element.MultVelAtaque;

                speedMultResult += element.MultSpeed;
            }

        }

        player.MaxLife = (uint)MathF.Max(one, maxLifeResult);

        CorrectLife(maxOld, player.MaxLife);

        player.Damage = (uint)MathF.Max(zero, dmgResult);

        MultiplicadorConciencia = MathF.Max(minConciencia, multConcienciaResult);

        player.TGPC = (uint)MathF.Max(one, tgpcResult);

        player.CritProb = (byte)MathF.Max(zero, critProbResult);

        player.CritMult = MathF.Max(one, critMultResult);

        player.RoboVida = (byte)MathF.Max(zero, roboVidaResult);

        player.MultVelAtaque = MathF.Max(one, multVelAtaque);

        player.SpeedMult = MathF.Max(one, speedMultResult);
    }

    public void ResetStats()
    {
        uint maxOld = player.MaxLife;
        player.MaxLife = maxLife;
        player.Damage = dmg;
        MultiplicadorConciencia = one;
        player.TGPC = one;
        player.CritProb = zero;
        player.CritMult = three;
        player.RoboVida = zero;
        player.MultVelAtaque = one;
        player.SpeedMult = one;
        CorrectLife(maxOld, player.MaxLife);
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
        player.Life = (uint)Mathf.Max(zero, opVida);

        int opConci = (int)(player.Conciencia - (value * multiplicadorConciencia));
        player.Conciencia = (ushort)Mathf.Max(zero,opConci);
    }

    void Inconciencia()
    {
        if(contador>=fifty)
        {
            contador = zero;
        }

        if(contador == zero && enableTGPC == true && player.Status == estado.Dormido && player.Conciencia < player.MaxConciencia)
        {
            suma = player.TGPC * multTGPC;
            player.Conciencia += (ushort)suma;
            multTGPC++;
        }

        if(enableTGPC)
        {
            contador++;
        }

        if(Player.Conciencia>=player.MaxConciencia)
        {
            suma = zero;
            multTGPC = one;
        }
    }

    #endregion
}