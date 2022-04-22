using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[System.Serializable]
public class GameMaster : MonoBehaviour
{
    //Singleton
    public static GameMaster instance;

    [Header("Valores Iniciales (Modificables)")]
    [SerializeField]
    private uint dmg;
    [SerializeField]
    private float multiplicadorConciencia = 1.0f;
    public uint maxLife { get; private set; } = 100;
    
    //Referencias Al Jugador
    Player player;
    public GameObject playerObject;
    public GameObject particles, particlesChild;

    //Modificadores
    public List<Mods> mods = new List<Mods>();

    #region"Inputs Editor"
    //Valores Custom Inspector
    [HideInInspector]
    public string nameINP;
    [HideInInspector]
    public sbyte vidaINP, dmgINP, tgpcINP, critProbINP, roboDeVidaINP, multPesadillaINP;
    [HideInInspector]
    public float multConcienciaINP, critMultINP, multVelAtaqueINP, speedMultINP;
    [HideInInspector]
    public int damageToPlayer;
    #endregion

    //Variables de utilidad
    byte hundred = 100, one = 1, porcentual = 10, zero = 0, fifty = 50, contador = 0, multTGPC = 1, three = 3;
    float suma = 0f, minConciencia = 0.1f;
    bool nightmareCalled = false;
    public bool sceneReloaded = false;

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
        #region "Singleton"

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        player = new(maxLife, dmg);

        CheckMods();
    }


    private void FixedUpdate()
    {
        Inconciencia();

        if (playerObject == null && player.Life == zero && !sceneReloaded)
        {
            player = new Player(maxLife, dmg);
            Invoke("ReloadScene", one);
        }
    }

    private void Update()
    {
        if (player.Status == estado.Dormido)
        {
            player.Pesadilla = IsNightmare();
        }
        else
        {
            player.Pesadilla = false;
            nightmareCalled = false;
        }

        //Preguntar por los flags de cambio de estado en el jugador
        if (player.wakeFlag)
        {
            PlayerWake.Invoke();
            player.wakeFlag = false;

            if (particles != null)
            {
                particles.GetComponent<ParticleSystem>().Stop();
                particlesChild.GetComponent<ParticleSystem>().Stop();
            }
        }

        if(player.dreamFlag)
        {
            PlayerDream.Invoke();
            player.dreamFlag = false;

            if(particles != null)
            {
                particles.GetComponent<ParticleSystem>().Play();
                particlesChild.GetComponent<ParticleSystem>().Play();
            }
        }
    }

    #region"Manejo de Escenas"

    public void ReloadScene()
    {
        if (!sceneReloaded)
        {
            sceneReloaded = true;

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    #endregion

    #region"Sistema de posturas y técnicas"
    [Header("Postura del sueño")]
    public PosturaDelSueño posturaDelSueño;
    public InicializadorSistemaPosturas.Postura IDPostura;


    /// <summary>
    /// Aplica las técnicas de la postura del sueño de acuerdo al estado actual del jugador y el estado activo donde actúa la técnica.
    /// </summary>
    public void ApplyTechniques()
    {


        CheckStance();
        foreach (ModsTecnicas technique in posturaDelSueño.Techniques)
        {
            switch(player.Status)
            {
                case estado.Despierto:
                    if (technique.activeState == ModsTecnicas.ActiveState.awake) AddTechnique(technique);
                    break;

                case estado.Dormido:
                    if (technique.activeState == ModsTecnicas.ActiveState.anyDream) AddTechnique(technique);
                    else if (!IsNightmare() && technique.activeState == ModsTecnicas.ActiveState.normalDream) AddTechnique(technique);
                    else if (IsNightmare() && technique.activeState == ModsTecnicas.ActiveState.nightmareDream) AddTechnique(technique);
                    break;
                default:
                    Debug.LogWarning("|GameMaster -> Sistema de posturas| Algo salio mal con el estado del jugador");
                    break;
            }
        }
    }

    /// <summary>
    /// Agrega un modificador creado a partir de una técnica a la lista de modificadores.
    /// </summary>
    /// <param name="technique">La técnica que se usará para crear el modificador a agregar</param>
    void AddTechnique(ModsTecnicas technique)
    {
        try
        {
            AddMod(technique.techniqueName, technique.multVidaMax, technique.multDmg, technique.multConciencia, technique.multTGPC,
            technique.multCritProb, technique.multCrit, technique.multRoboPer, technique.multVelAatque, technique.multSpeed, technique.multPesadillaPer);
        }
        catch (Exception)
        {
            Debug.LogError("|GameMaster -> Sistema de posturas| No se pudo añadir el modificador de la técnica");
        }
    }

    /// <summary>
    /// Busca y remueve todas las técnicas(modificadores) que estén en la lista de modificadores. Este método siempre deberia remover al menos una técnica debido a que el método ApplyTechniques siempre agrega al menos un modificador.
    /// </summary>
    public void RemoveActiveTechniques()
    {
        CheckStance();
        bool managedToRemove = false;
        foreach (ModsTecnicas technique in posturaDelSueño.Techniques)
        {
            foreach (Mods mod in mods)
            {
                if (Equals(technique.techniqueName, mod.Name))
                {
                    mods.Remove(mod);
                    managedToRemove = true;
                    break;
                }
            }
        }
        CheckMods();
        if (!managedToRemove) Debug.LogWarning("|GameMaster -> Sistema de posturas| No se encontraron modificadores de técnicas a remover");
    }

    /// <summary>
    /// Verifica que el GameMaster tenga una postura asignada, si no es así, este método lanza una alerta a la consola.
    /// </summary>
    void CheckStance()
    {
        if (posturaDelSueño == null) Debug.LogWarning("|GameMaster -> Sistema de posturas| No se encontró la postura que debería poseer el jugador");
    }

    #endregion

    #region"Sistema de eventos"
    public UnityEvent PlayerDream;

    public UnityEvent PlayerWake;

    public UnityEvent OnRoomFinished;
    #endregion

    #region"Sistema de modificadores y Estadisticas"

    public void AddMod(string name, sbyte vida, sbyte dmg, float multConciencia, sbyte multTGPC, sbyte critProb, float critMult,
        sbyte multRoboPer, float multVelAtaque, float multSpeed, sbyte multPesadillaPer)
    {
        mods.Add(new Mods(name, (sbyte)vida, (sbyte)dmg, multConciencia, (sbyte)multTGPC, critProb, critMult, multRoboPer, multVelAtaque, multSpeed, multPesadillaPer));

        CheckMods();

        Debug.Log(mods.Count);
    }

    public void RemoveMod(string name)
    {
        foreach(Mods element in mods)
        {
            if(element.Name == name)
            {
                mods.Remove(element);
                return;
            }
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
        float multPesadillaResult = zero;

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

                multPesadillaResult += element.MultPesadillaPer;
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

        player.CritProb = (byte)MathF.Max(zero, critProbResult);

        player.MultPesadilla = (byte)MathF.Max(zero, multPesadillaResult);
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
        player.MultPesadilla = zero;
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

    public int CalculateSpellDamage(float spellDamage)
    {
        int totalDamage = (int)(spellDamage * player.Damage); 

        return totalDamage;
    }

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
            nightmareCalled = false;
        }
    }

    bool IsNightmare()
    {
        if(!nightmareCalled && player.Conciencia<=zero)
        {
            nightmareCalled = true;

            float rnd = UnityEngine.Random.Range(zero, hundred);

            if (rnd < player.MultPesadilla || rnd == hundred)
            {
                Debug.Log("Fue Pesadilla");
                return true;
            }
        }

        return player.Pesadilla;
    }

    #endregion
}