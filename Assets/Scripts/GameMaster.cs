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

    public GameObject DamagePopUp;

    //Referencias Al Jugador
    Player player;
    public GameObject playerObject;
    public GameObject sleepParticle;
    public GameObject sleepParticlePrefab;

    //Modificadores
    public List<Mods> mods = new List<Mods>();

    #region"Inputs Editor"
    //Valores Custom Inspector
    [HideInInspector]
    public string nameINP;
    [HideInInspector]
    public sbyte vidaINP, dmgINP, tgpcINP, critProbINP, roboDeVidaINP, multPesadillaINP;
    [HideInInspector]
    public float multConcienciaINP, critMultINP, multVelAtaqueINP, speedMultINP, multDa�oRecibidoINP, multHechizosINP;
    [HideInInspector]
    public int damageToPlayer;
    [HideInInspector]
    public DreamCatcher dmrcatcherINP;
    #endregion

    //Variables de utilidad
    byte hundred = 100, one = 1, porcentual = 10, zero = 0, fifty = 50, contador = 0, multTGPC = 1, three = 3;
    float suma = 0f, minConciencia = 0.1f;
    bool nightmareCalled = false;
    public bool sceneReloaded = false;

    public Texture2D cursorTexture, cursorTexture2;

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

        player = new Player(maxLife, dmg);
        CheckMods();
    }


    private void FixedUpdate()
    {
        Inconciencia();
    }

    private void Update()
    {
        if (playerObject == null && !sceneReloaded)
        {
            sceneReloaded = true;
            player = new Player(maxLife, dmg);
            print("entro");
            Invoke("ReloadScene", one);
        }

        /*
        if (player.Status == estado.Dormido)
        {
            player.Pesadilla = IsNightmare();
        }
        else
        {
            player.Pesadilla = false;
            nightmareCalled = false;
        }*/

        //Preguntar por los flags de cambio de estado en el jugador
        if (player.wakeFlag)
        {
            PlayerWake.Invoke();
            player.wakeFlag = false;

            player.Pesadilla = false;
            nightmareCalled = false;

            if (sleepParticle != null)
            {
                sleepParticle.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();

                ParticleSystem[] particles = sleepParticle.transform.GetChild(0).GetComponentsInChildren<ParticleSystem>();
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].Stop();
                }
            }
        }

        if(player.dreamFlag)
        {
            player.Pesadilla = IsNightmare();
            player.dreamFlag = false;
            PlayerDream.Invoke();
            

            
            print(player.Pesadilla + " on Event call");

            if (sleepParticle != null)
            {
                sleepParticle.transform.GetChild(0).GetComponent<ParticleSystem>().Play();

                ParticleSystem[] particles = sleepParticle.transform.GetChild(0).GetComponentsInChildren<ParticleSystem>();
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].Play();
                }
            }
        }
    }

    #region"Manejo de Escenas"

    public void ReloadScene()
    {
        SceneManager.LoadScene(2);
        sceneReloaded = false;
        print("se corrio");
    }

    #endregion

    #region"Sistema de posturas y t�cnicas"
    [Header("Postura del sue�o")]
    public PosturaDelSue�o posturaDelSue�o;
    public Postura IDPostura;


    /// <summary>
    /// Aplica las t�cnicas de la postura del sue�o de acuerdo al estado actual del jugador y el estado activo donde act�a la t�cnica.
    /// </summary>
    public void ApplyTechniques()
    {        
        CheckStance();
        foreach (ModsTecnicas technique in posturaDelSue�o.Techniques)
        {
            switch(player.Status)
            {
                case estado.Despierto:
                    if (technique.activeState == ModsTecnicas.ActiveState.awake) AddTechnique(technique);
                    break;

                case estado.Dormido:
                    if (technique.activeState == ModsTecnicas.ActiveState.anyDream) AddTechnique(technique);
                    else if (!player.Pesadilla && technique.activeState == ModsTecnicas.ActiveState.normalDream) AddTechnique(technique);
                    else if (player.Pesadilla && technique.activeState == ModsTecnicas.ActiveState.nightmareDream) AddTechnique(technique);
                    break;
                default:
                    Debug.LogWarning("|GameMaster -> Sistema de posturas| Algo salio mal con el estado del jugador");
                    break;
            }
        }
    }

    /// <summary>
    /// Agrega un modificador creado a partir de una t�cnica a la lista de modificadores.
    /// </summary>
    /// <param name="technique">La t�cnica que se usar� para crear el modificador a agregar</param>
    void AddTechnique(ModsTecnicas technique)
    {
        try
        {
            AddMod(technique.techniqueName, technique.multVidaMax, technique.multDmg, technique.multConciencia, technique.multTGPC,
            technique.multCritProb, technique.multCrit, technique.multRoboPer, technique.multVelAatque, technique.multSpeed, technique.multPesadillaPer, technique.multDa�oRecibido, technique.multHechizos);
        }
        catch (Exception)
        {
            Debug.LogError("|GameMaster -> Sistema de posturas| No se pudo a�adir el modificador de la t�cnica");
        }
    }

    /// <summary>
    /// Busca y remueve todas las t�cnicas(modificadores) que est�n en la lista de modificadores. Este m�todo siempre deberia remover al menos una t�cnica debido a que el m�todo ApplyTechniques siempre agrega al menos un modificador.
    /// </summary>
    public void RemoveActiveTechniques()
    {
        CheckStance();
        bool managedToRemove = false;
        if (posturaDelSue�o == null) return;
        foreach (ModsTecnicas technique in posturaDelSue�o.Techniques)
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
        if (!managedToRemove) Debug.LogWarning("|GameMaster -> Sistema de posturas| No se encontraron modificadores de t�cnicas a remover");
    }

    /// <summary>
    /// Verifica que el GameMaster tenga una postura asignada, si no es as�, este m�todo lanza una alerta a la consola.
    /// </summary>
    void CheckStance()
    {
        if (posturaDelSue�o == null) Debug.LogWarning("|GameMaster -> Sistema de posturas| No se encontr� la postura que deber�a poseer el jugador");
    }

    #endregion

    #region"Sistema de eventos"
    public UnityEvent PlayerDream;

    public UnityEvent PlayerWake;

    public UnityEvent OnRoomFinished;

    public UnityEvent OnRunEnd;
    #endregion

    #region"Sistema de modificadores y Estadisticas"

    public void AddMod(string name, sbyte vida, sbyte dmg, float multConciencia, sbyte multTGPC, sbyte critProb, float critMult,
        sbyte multRoboPer, float multVelAtaque, float multSpeed, sbyte multPesadillaPer, float multDa�oRecibido, float multHechizos)
    {
        mods.Add(new Mods(name, (sbyte)vida, (sbyte)dmg, multConciencia, (sbyte)multTGPC, critProb, critMult, multRoboPer, multVelAtaque, multSpeed, multPesadillaPer, multDa�oRecibido, multHechizos));

        CheckMods();

        Debug.Log(mods.Count);
    }

    public void RemoveMod(string name)
    {
        foreach(Mods element in mods)
        {
            if(Equals(element.Name,name))
            {
                mods.Remove(element);
                CheckMods();
                return;
            }
        }
    }

    public void RemoveAllMods()
    {
        for (int i = 0; i < mods.Count; i++)
        {
            mods.Remove(mods[i]);
            CheckMods();
        }
    }

    public void CheckMods()
    {
        float maxLifeResult = zero;
        float dmgResult = zero;
        float multConcienciaResult = one;
        float tgpcResult = one;
        float critProbResult = zero;
        float critMultResult = three;
        float roboVidaResult = zero;
        float multVelAtaque = one;
        float speedMultResult = one;
        float multPesadillaResult = zero;
        float multDa�oRecibidoResult = one;
        float multHechizos = one;

        uint maxOld = player.MaxLife;

        if(mods.Count != zero)
        {
            foreach (Mods element in mods)
            {
                maxLifeResult += element.MultVidaMax;

                dmgResult += element.MultDmg;

                multConcienciaResult += element.MultConciencia;

                tgpcResult += element.MultTGPC;

                critProbResult += element.MultCritProb;

                critMultResult += element.MultCrit;

                roboVidaResult += element.MultRoboPer;

                multVelAtaque += element.MultVelAtaque;

                speedMultResult += element.MultSpeed;

                multPesadillaResult += element.MultPesadillaPer;

                multDa�oRecibidoResult += element.MultDa�oRecibido;

                multHechizos += element.MultHechizos;
            }
        }

        player.MaxLife = (uint)MathF.Max(one, maxLife + (maxLife * (maxLifeResult * porcentual)) / hundred);

        CorrectLife(maxOld, player.MaxLife);

        player.Damage = (uint)MathF.Max(one, dmg + (dmg * (dmgResult * porcentual)) / hundred);

        MultiplicadorConciencia = MathF.Max(minConciencia, multConcienciaResult);

        player.TGPC = (uint)MathF.Max(one, tgpcResult);

        player.CritProb = (byte)MathF.Max(zero, critProbResult);

        player.CritMult = MathF.Max(one, critMultResult);

        player.RoboVida = (byte)MathF.Max(zero, roboVidaResult);

        player.MultVelAtaque = MathF.Max(one, multVelAtaque);

        player.SpeedMult = MathF.Max(one, speedMultResult);

        player.CritProb = (byte)MathF.Max(zero, critProbResult);

        player.MultPesadilla = (byte)MathF.Max(zero, multPesadillaResult);

        player.MultDa�oRecibido = MathF.Max(-one, multDa�oRecibidoResult);

        player.MultHechizos = MathF.Max(one, multHechizos);
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
        player.MultDa�oRecibido = one;
        player.MultHechizos = one;
        CorrectLife(maxOld, player.MaxLife);

        player.Conciencia = 100;
        player.Life = player.MaxLife;

        player.dreamFlag = false;
        player.wakeFlag = false;

        if (player.status == estado.Dormido)
        {
            PlayerWake.Invoke();
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

    public int CalculateSpellDamage(float spellDamage)
    {
        int totalDamage = (int)(spellDamage * player.Damage); 

        return totalDamage;
    }

    #endregion

    #region"Acciones hacia el jugador"

    public void DamagePlayer(int damageAmount, int conscienceAmount)
    {
        int deltaVida = (int)(damageAmount * player.MultDa�oRecibido);
        int opVida = (int)(player.Life - (deltaVida));
        player.Life = (uint)Mathf.Max(zero, opVida);

        int deltaConsciencia;

        if (player.Status != estado.Dormido)
        {
            deltaConsciencia = (int)(conscienceAmount * multiplicadorConciencia);
            int opConci = (int)(player.Conciencia - (deltaConsciencia));
            player.Conciencia = (ushort)Mathf.Max(zero, opConci);
        }
        else
        {
            deltaConsciencia = (int)(conscienceAmount * multiplicadorConciencia);
            int opConci = (int)(player.Conciencia + (deltaConsciencia));
            player.Conciencia = (ushort)Mathf.Max(zero, opConci);
        }
        
        // Damage pop up
        GameObject popUpInstace = Instantiate(DamagePopUp, playerObject.transform.position + Vector3.up * 1.5f + Vector3.right, DamagePopUp.transform.rotation);
        popUpInstace.GetComponent<DamagePopUp>().SetText(AttackType.normal, deltaVida);

        // Conscience pop up
        popUpInstace = Instantiate(DamagePopUp, playerObject.transform.position + Vector3.up * 2f + Vector3.right, DamagePopUp.transform.rotation);
        popUpInstace.GetComponent<DamagePopUp>().SetText(AttackType.conscience, deltaConsciencia);
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

        return false;
    }
    #endregion
}