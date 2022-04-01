using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManagerHechizos : MonoBehaviour
{
    public UnityEvent FirstSpellCast;
    public UnityEvent SecondSpellCast;
    public UnityEvent ThirdSpellCast;

    public List<Hechizo> spellsData;
    public List<MonoBehaviour> availableSpells;

    public List<Hechizo> debugSpellData;

    public void AddNewSpell(Hechizo hechizo)
    {      
        AddSpell(hechizo);
    }

    void AddSpell(Hechizo hechizo)
    {
        if (availableSpells.Count > 2 )
        {
            SwapLastSpellForNewSpell(hechizo);
        }
        else
        {
            spellsData.Add(hechizo);

            UnityEvent ue = null;
            switch (spellsData.Count)
            {
                case 1:
                    ue = FirstSpellCast;
                    break;

                case 2:
                    ue = SecondSpellCast;
                    break;

                case 3:
                    ue = ThirdSpellCast;
                    break;

                default:
                    Debug.LogError("|Manager hechizos| No se pudo preparar el UnityEvent para subscribir al hechizo");
                    break;
            }
            #region"Agregar el componente que contiene IHechizo"
            switch (spellsData[spellsData.Count - 1].spellContained)
            {
                case Hechizo.EHechizo.BolaDeFuego:
                    BolaDeFuego fireball = gameObject.AddComponent<BolaDeFuego>();
                    fireball.SubscribeToEvent(ue);
                    availableSpells.Add(fireball);
                    break;

                case Hechizo.EHechizo.FlechaDeFuego:
                    FlechaDeFuego fireArrow = gameObject.AddComponent<FlechaDeFuego>();
                    fireArrow.SubscribeToEvent(ue);
                    availableSpells.Add(fireArrow);
                    break;

                case Hechizo.EHechizo.EspadaDeLuz:
                    EspadaDeLuz lightSword = gameObject.AddComponent<EspadaDeLuz>();
                    lightSword.SubscribeToEvent(ue);
                    availableSpells.Add(lightSword);
                    break;

                case Hechizo.EHechizo.PedradaMagica:
                    PedradaMagica magicpebble = gameObject.AddComponent<PedradaMagica>();
                    magicpebble.SubscribeToEvent(ue);
                    availableSpells.Add(magicpebble);
                    break;

                case Hechizo.EHechizo.BoomerangDeEnergia:
                    BoomerangEnergia energyBoomerang = gameObject.AddComponent<BoomerangEnergia>();
                    energyBoomerang.SubscribeToEvent(ue);
                    availableSpells.Add(energyBoomerang);
                    break;

                case Hechizo.EHechizo.BolaDeAcido:
                    BolaDeAcido acidball = gameObject.AddComponent<BolaDeAcido>();
                    acidball.SubscribeToEvent(ue);
                    availableSpells.Add(acidball);
                    break;

                case Hechizo.EHechizo.DashElectrico:
                    DashElectrico electricDash = gameObject.AddComponent<DashElectrico>();
                    electricDash.SubscribeToEvent(ue);
                    availableSpells.Add(electricDash);
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

        availableSpells.Clear();
        spellsData.Clear();
    }

    public void SwapSpellHotkeyAndInventoryPosition(int selectedOriginalSlot, int newSelectedSlot)
    {
        UnityEvent[] spellCastEvents = { FirstSpellCast, SecondSpellCast, ThirdSpellCast };

        print(availableSpells[0]);
        print(availableSpells[1]);
        print(availableSpells[2]);

        Hechizo tempSpellData = spellsData[selectedOriginalSlot];
        spellsData[selectedOriginalSlot] = spellsData[newSelectedSlot];
        spellsData[newSelectedSlot] = tempSpellData;

        spellCastEvents[selectedOriginalSlot].RemoveAllListeners();
        spellCastEvents[newSelectedSlot].RemoveAllListeners();

        Component tempSpell = availableSpells[selectedOriginalSlot];
        availableSpells[selectedOriginalSlot] = availableSpells[newSelectedSlot];
        availableSpells[newSelectedSlot] = (MonoBehaviour)tempSpell;

        print(availableSpells[0].GetComponent<IHechizo>());
        print(availableSpells[1].GetComponent<IHechizo>());
        print(availableSpells[2].GetComponent<IHechizo>());
        //availableSpells[0].GetComponent<IHechizo>().SubscribeToEvent(spellCastEvents[selectedOriginalSlot]);
        //availableSpells[newSelectedSlot].GetComponent<IHechizo>().SubscribeToEvent(spellCastEvents[newSelectedSlot]);
    }

    public void SwapLastSpellForNewSpell(Hechizo spell)
    {
        spellsData[2] = spell;
        ThirdSpellCast.RemoveAllListeners();
        availableSpells.RemoveAt(2);

        #region"Agregar el componente al final, que contiene IHechizo"
        switch (spell.spellContained)
        {
            case Hechizo.EHechizo.BolaDeFuego:
                BolaDeFuego fireball = gameObject.AddComponent<BolaDeFuego>();
                fireball.SubscribeToEvent(ThirdSpellCast);
                availableSpells.Add(fireball);
                break;

            case Hechizo.EHechizo.FlechaDeFuego:
                FlechaDeFuego fireArrow = gameObject.AddComponent<FlechaDeFuego>();
                fireArrow.SubscribeToEvent(ThirdSpellCast);
                availableSpells.Add(fireArrow);
                break;

            case Hechizo.EHechizo.EspadaDeLuz:
                EspadaDeLuz lightSword = gameObject.AddComponent<EspadaDeLuz>();
                lightSword.SubscribeToEvent(ThirdSpellCast);
                availableSpells.Add(lightSword);
                break;

            case Hechizo.EHechizo.PedradaMagica:
                PedradaMagica magicpebble = gameObject.AddComponent<PedradaMagica>();
                magicpebble.SubscribeToEvent(ThirdSpellCast);
                availableSpells.Add(magicpebble);
                break;

            case Hechizo.EHechizo.BoomerangDeEnergia:
                BoomerangEnergia energyBoomerang = gameObject.AddComponent<BoomerangEnergia>();
                energyBoomerang.SubscribeToEvent(ThirdSpellCast);
                availableSpells.Add(energyBoomerang);
                break;

            case Hechizo.EHechizo.BolaDeAcido:
                BolaDeAcido acidball = gameObject.AddComponent<BolaDeAcido>();
                acidball.SubscribeToEvent(ThirdSpellCast);
                availableSpells.Add(acidball);
                break;

            case Hechizo.EHechizo.DashElectrico:
                DashElectrico electricDash = gameObject.AddComponent<DashElectrico>();
                electricDash.SubscribeToEvent(ThirdSpellCast);
                availableSpells.Add(electricDash);
                break;

            default:
                Debug.LogError("|Manager hechizos| No se pudo encontrar el hechizo");
                break;
        }
        #endregion

    }

    private void Update()
    {
        //Test debugs
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FirstSpellCast.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SecondSpellCast.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ThirdSpellCast.Invoke();
        }

        #region"Input para Debug de hechizos"
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
            SwapSpellHotkeyAndInventoryPosition(2, 3);
        }

        if (Input.GetKeyDown(KeyCode.H)) // 2 <- 3
        {
            SwapSpellHotkeyAndInventoryPosition(3, 2);
        }

        if (Input.GetKeyDown(KeyCode.V)) // 1 -> 3
        {
            SwapSpellHotkeyAndInventoryPosition(1, 3);
        }

        if (Input.GetKeyDown(KeyCode.N)) // 1 <- 3
        {
            SwapSpellHotkeyAndInventoryPosition(3, 1);
        }

        #endregion
    }
}
