using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ManagerHechizos : MonoBehaviour
{
    //Singleton
    public static ManagerHechizos instance;
    
    public UnityEvent FirstSpellCast;
    public UnityEvent SecondSpellCast;
    public UnityEvent ThirdSpellCast;

    public Hechizo[] spellsData;
    public MonoBehaviour[] availableSpells;

    public Hechizo[] debugSpellData;

    UI_SlotsHechizos slotsHechizos;

    // Variable de control
    public bool castingSpell = false;

    private void Awake()
    {
        slotsHechizos = FindObjectOfType<UI_SlotsHechizos>();

        if (slotsHechizos == null) Debug.LogWarning("|Manager hechizos| No se pudo encontrar la referencia a SlotsHechizos");

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        UpdateSpellSlots();
    }

    int GetIndex_of_NearestEmptySpellSlot()
    {
        int index = 0;

        for (int i = 0; i < spellsData.Length; i++)
        {
            if (spellsData[i] == null)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    int GetAmount_of_SpellsInPlayer()
    {
        int amount = 0;

        for (int i = 0; i < spellsData.Length; i++)
        {
            if (spellsData[i] != null) amount++;
        }

        return amount;
    }

    public void AddNewSpell(Hechizo hechizo)
    {      
        AddSpell(hechizo);
        UpdateSpellSlots();
    }

    void AddSpell(Hechizo hechizo)
    {
        int index_NearestEmptySpellSlot = GetIndex_of_NearestEmptySpellSlot();

        if (GetAmount_of_SpellsInPlayer() > 2)
        {
            SwapLastSpellForNewSpell(hechizo);
        }
        else
        {
            spellsData[index_NearestEmptySpellSlot] = hechizo;

            UnityEvent ue = null;
            switch (index_NearestEmptySpellSlot)
            {
                case 0:
                    ue = FirstSpellCast;
                    break;

                case 1:
                    ue = SecondSpellCast;
                    break;

                case 2:
                    ue = ThirdSpellCast;
                    break;

                default:
                    Debug.LogError("|Manager hechizos| No se pudo preparar el UnityEvent para subscribir al hechizo");
                    break;
            }
            #region"Agregar el componente que contiene IHechizo"
            switch (spellsData[index_NearestEmptySpellSlot].spellContained)
            {
                case Hechizo.EHechizo.BolaDeFuego:
                    BolaDeFuego fireball = gameObject.AddComponent<BolaDeFuego>();
                    fireball.SubscribeToEvent(ue);
                    availableSpells[index_NearestEmptySpellSlot] = fireball;
                    break;

                case Hechizo.EHechizo.FlechaDeFuego:
                    FlechaDeFuego fireArrow = gameObject.AddComponent<FlechaDeFuego>();
                    fireArrow.SubscribeToEvent(ue);
                    availableSpells[index_NearestEmptySpellSlot] = fireArrow;
                    break;

                case Hechizo.EHechizo.EspadaDeLuz:
                    EspadaDeLuz lightSword = gameObject.AddComponent<EspadaDeLuz>();
                    lightSword.SubscribeToEvent(ue);
                    availableSpells[index_NearestEmptySpellSlot] = lightSword;
                    break;

                case Hechizo.EHechizo.PedradaMagica:
                    PedradaMagica magicpebble = gameObject.AddComponent<PedradaMagica>();
                    magicpebble.SubscribeToEvent(ue);
                    availableSpells[index_NearestEmptySpellSlot] = magicpebble;
                    break;

                case Hechizo.EHechizo.BoomerangDeEnergia:
                    BoomerangEnergia energyBoomerang = gameObject.AddComponent<BoomerangEnergia>();
                    energyBoomerang.SubscribeToEvent(ue);
                    availableSpells[index_NearestEmptySpellSlot] = energyBoomerang;
                    break;

                case Hechizo.EHechizo.BolaDeAcido:
                    BolaDeAcido acidball = gameObject.AddComponent<BolaDeAcido>();
                    acidball.SubscribeToEvent(ue);
                    availableSpells[index_NearestEmptySpellSlot] = acidball;
                    break;

                case Hechizo.EHechizo.DashElectrico:
                    DashElectrico electricDash = gameObject.AddComponent<DashElectrico>();
                    electricDash.SubscribeToEvent(ue);
                    availableSpells[index_NearestEmptySpellSlot] = electricDash;
                    break;

                default:
                    Debug.LogError("|Manager hechizos| No se pudo encontrar el hechizo");
                    break;
            }
            #endregion
        }
    }

    public void CleanAllSpells()
    {
        //Remover Listeners de todos los eventos
        FirstSpellCast.RemoveAllListeners();
        SecondSpellCast.RemoveAllListeners();
        ThirdSpellCast.RemoveAllListeners();

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            if (script is IHechizo)
            {
                Destroy(script);
            }
        }

        Array.Clear(availableSpells, 0, availableSpells.Length);
        Array.Clear(spellsData, 0, spellsData.Length);

        UpdateSpellSlots();
    }

    public void SwapSpellHotkeyAndInventoryPosition(int selectedOriginalSlot, int newSelectedSlot)
    {
        UnityEvent[] spellCastEvents = { FirstSpellCast, SecondSpellCast, ThirdSpellCast };

        //Remover todos los listeners
        spellCastEvents[selectedOriginalSlot].RemoveAllListeners();
        spellCastEvents[newSelectedSlot].RemoveAllListeners();

        Hechizo tempSpellData = spellsData[selectedOriginalSlot];
        spellsData[selectedOriginalSlot] = spellsData[newSelectedSlot];
        spellsData[newSelectedSlot] = tempSpellData;

        Component tempSpell = availableSpells[selectedOriginalSlot];
        availableSpells[selectedOriginalSlot] = availableSpells[newSelectedSlot];
        availableSpells[newSelectedSlot] = (MonoBehaviour)tempSpell;

        Destroy(availableSpells[selectedOriginalSlot]);
        Destroy(availableSpells[newSelectedSlot]);

        //Jueputa
        #region"Agregar los componentes a su nuevas posiciones, instanciadolos de nuevo"


        if (spellsData[selectedOriginalSlot] != null)
        {
            switch (spellsData[selectedOriginalSlot].spellContained)
            {
                case Hechizo.EHechizo.BolaDeFuego:
                    BolaDeFuego fireball = gameObject.AddComponent<BolaDeFuego>();
                    fireball.SubscribeToEvent(spellCastEvents[selectedOriginalSlot]);
                    availableSpells[selectedOriginalSlot] = fireball;
                    break;

                case Hechizo.EHechizo.FlechaDeFuego:
                    FlechaDeFuego fireArrow = gameObject.AddComponent<FlechaDeFuego>();
                    fireArrow.SubscribeToEvent(spellCastEvents[selectedOriginalSlot]);
                    availableSpells[selectedOriginalSlot] = fireArrow;
                    break;

                case Hechizo.EHechizo.EspadaDeLuz:
                    EspadaDeLuz lightSword = gameObject.AddComponent<EspadaDeLuz>();
                    lightSword.SubscribeToEvent(spellCastEvents[selectedOriginalSlot]);
                    availableSpells[selectedOriginalSlot] = lightSword;
                    break;

                case Hechizo.EHechizo.PedradaMagica:
                    PedradaMagica magicpebble = gameObject.AddComponent<PedradaMagica>();
                    magicpebble.SubscribeToEvent(spellCastEvents[selectedOriginalSlot]);
                    availableSpells[selectedOriginalSlot] = magicpebble;
                    break;

                case Hechizo.EHechizo.BoomerangDeEnergia:
                    BoomerangEnergia energyBoomerang = gameObject.AddComponent<BoomerangEnergia>();
                    energyBoomerang.SubscribeToEvent(spellCastEvents[selectedOriginalSlot]);
                    availableSpells[selectedOriginalSlot] = energyBoomerang;
                    break;

                case Hechizo.EHechizo.BolaDeAcido:
                    BolaDeAcido acidball = gameObject.AddComponent<BolaDeAcido>();
                    acidball.SubscribeToEvent(spellCastEvents[selectedOriginalSlot]);
                    availableSpells[selectedOriginalSlot] = acidball;
                    break;

                case Hechizo.EHechizo.DashElectrico:
                    DashElectrico electricDash = gameObject.AddComponent<DashElectrico>();
                    electricDash.SubscribeToEvent(spellCastEvents[selectedOriginalSlot]);
                    availableSpells[selectedOriginalSlot] = electricDash;
                    break;

                default:
                    Debug.LogError("|Manager hechizos| No se pudo encontrar el hechizo");
                    break;
            }
        }

        if (spellsData[newSelectedSlot] != null)
        {
            switch (spellsData[newSelectedSlot].spellContained)
            {
                case Hechizo.EHechizo.BolaDeFuego:
                    BolaDeFuego fireball = gameObject.AddComponent<BolaDeFuego>();
                    fireball.SubscribeToEvent(spellCastEvents[newSelectedSlot]);
                    availableSpells[newSelectedSlot] = fireball;
                    break;

                case Hechizo.EHechizo.FlechaDeFuego:
                    FlechaDeFuego fireArrow = gameObject.AddComponent<FlechaDeFuego>();
                    fireArrow.SubscribeToEvent(spellCastEvents[newSelectedSlot]);
                    availableSpells[newSelectedSlot] = fireArrow;
                    break;

                case Hechizo.EHechizo.EspadaDeLuz:
                    EspadaDeLuz lightSword = gameObject.AddComponent<EspadaDeLuz>();
                    lightSword.SubscribeToEvent(spellCastEvents[newSelectedSlot]);
                    availableSpells[newSelectedSlot] = lightSword;
                    break;

                case Hechizo.EHechizo.PedradaMagica:
                    PedradaMagica magicpebble = gameObject.AddComponent<PedradaMagica>();
                    magicpebble.SubscribeToEvent(spellCastEvents[newSelectedSlot]);
                    availableSpells[newSelectedSlot] = magicpebble;
                    break;

                case Hechizo.EHechizo.BoomerangDeEnergia:
                    BoomerangEnergia energyBoomerang = gameObject.AddComponent<BoomerangEnergia>();
                    energyBoomerang.SubscribeToEvent(spellCastEvents[newSelectedSlot]);
                    availableSpells[newSelectedSlot] = energyBoomerang;
                    break;

                case Hechizo.EHechizo.BolaDeAcido:
                    BolaDeAcido acidball = gameObject.AddComponent<BolaDeAcido>();
                    acidball.SubscribeToEvent(spellCastEvents[newSelectedSlot]);
                    availableSpells[newSelectedSlot] = acidball;
                    break;

                case Hechizo.EHechizo.DashElectrico:
                    DashElectrico electricDash = gameObject.AddComponent<DashElectrico>();
                    electricDash.SubscribeToEvent(spellCastEvents[newSelectedSlot]);
                    availableSpells[newSelectedSlot] = electricDash;
                    break;

                default:
                    Debug.LogError("|Manager hechizos| No se pudo encontrar el hechizo");
                    break;
            }
        }
        
        #endregion
        UpdateSpellSlots();
    }

    public void SwapLastSpellForNewSpell(Hechizo spell)
    {
        int lastIndex = 2;

        Destroy(availableSpells[lastIndex]);
        spellsData[lastIndex] = spell;
        ThirdSpellCast.RemoveAllListeners();
        availableSpells[lastIndex] = null;

        
        #region"Agregar el componente al final, que contiene IHechizo"
        switch (spell.spellContained)
        {
            case Hechizo.EHechizo.BolaDeFuego:
                BolaDeFuego fireball = gameObject.AddComponent<BolaDeFuego>();
                fireball.SubscribeToEvent(ThirdSpellCast);
                availableSpells[lastIndex] = fireball;
                break;

            case Hechizo.EHechizo.FlechaDeFuego:
                FlechaDeFuego fireArrow = gameObject.AddComponent<FlechaDeFuego>();
                fireArrow.SubscribeToEvent(ThirdSpellCast);
                availableSpells[lastIndex] = fireArrow;
                break;

            case Hechizo.EHechizo.EspadaDeLuz:
                EspadaDeLuz lightSword = gameObject.AddComponent<EspadaDeLuz>();
                lightSword.SubscribeToEvent(ThirdSpellCast);
                availableSpells[lastIndex] = lightSword;
                break;

            case Hechizo.EHechizo.PedradaMagica:
                PedradaMagica magicpebble = gameObject.AddComponent<PedradaMagica>();
                magicpebble.SubscribeToEvent(ThirdSpellCast);
                availableSpells[lastIndex] = magicpebble;
                break;

            case Hechizo.EHechizo.BoomerangDeEnergia:
                BoomerangEnergia energyBoomerang = gameObject.AddComponent<BoomerangEnergia>();
                energyBoomerang.SubscribeToEvent(ThirdSpellCast);
                availableSpells[lastIndex] = energyBoomerang;
                break;

            case Hechizo.EHechizo.BolaDeAcido:
                BolaDeAcido acidball = gameObject.AddComponent<BolaDeAcido>();
                acidball.SubscribeToEvent(ThirdSpellCast);
                availableSpells[lastIndex] = acidball;
                break;

            case Hechizo.EHechizo.DashElectrico:
                DashElectrico electricDash = gameObject.AddComponent<DashElectrico>();
                electricDash.SubscribeToEvent(ThirdSpellCast);
                availableSpells[lastIndex] = electricDash;
                break;

            default:
                Debug.LogError("|Manager hechizos| No se pudo encontrar el hechizo");
                break;
        }
        #endregion

        UpdateSpellSlots();
    }

    public void UpdateSpellSlots()
    {
        for (int i = 0; i < spellsData.Length; i++)
        {
            if (spellsData[i] == null)
            {
                slotsHechizos.slotsImages[i].color = new Color(0, 0, 0, 0);
                slotsHechizos.slotsImages2[i].color = new Color(0, 0, 0, 0);

            }
            else
            {
                slotsHechizos.slotsImages[i].color = Color.white;
                slotsHechizos.slotsImages2[i].color = Color.white;
            }
        }

        for (int i = 0; i < spellsData.Length; i++)
        {
            if (spellsData[i] != null)
            {
                slotsHechizos.hechizos[i] = spellsData[i];
                slotsHechizos.slotsImages[i].sprite = spellsData[i].sprite;
                slotsHechizos.slotsImages2[i].sprite = spellsData[i].sprite;
            }
        }
    }

    private void Update()
    {

        if (!castingSpell)
        {
            
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            FirstSpellCast.Invoke();
            StartSpellCast();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SecondSpellCast.Invoke();
            StartSpellCast();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ThirdSpellCast.Invoke();
            StartSpellCast();
        }
        #region"Input para Debug de hechizos"

        /*
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            CleanAllSpells();
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            AddNewSpell(debugSpellData[0]);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            AddNewSpell(debugSpellData[1]);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            AddNewSpell(debugSpellData[2]);
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            AddNewSpell(debugSpellData[3]);
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            AddNewSpell(debugSpellData[4]);
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            AddNewSpell(debugSpellData[5]);
        }

        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            AddNewSpell(debugSpellData[6]);
        }

        if (Input.GetKeyDown(KeyCode.R)) // 1 -> 2
        {
            SwapSpellHotkeyAndInventoryPosition(0, 1);
        }

        if (Input.GetKeyDown(KeyCode.T)) // 1 <- 2
        {
            SwapSpellHotkeyAndInventoryPosition(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.G)) // 2 -> 3
        {
            SwapSpellHotkeyAndInventoryPosition(1, 2);
        }

        if (Input.GetKeyDown(KeyCode.H)) // 2 <- 3
        {
            SwapSpellHotkeyAndInventoryPosition(2, 1);
        }

        if (Input.GetKeyDown(KeyCode.V)) // 1 -> 3
        {
            SwapSpellHotkeyAndInventoryPosition(0, 2);
        }

        if (Input.GetKeyDown(KeyCode.N)) // 1 <- 3
        {
            SwapSpellHotkeyAndInventoryPosition(2, 0);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            UpdateSpellSlots();
        }
        */

        #endregion
    }

    public void EndSpellCast()
    {
        castingSpell = false;
    }

    public void StartSpellCast()
    {
        castingSpell = true;
    }
}
