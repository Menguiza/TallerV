using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC_RogueFairy : MonoBehaviour, IVendorNPC, IInteractive
{
    [SerializeField] Hechizo[] spells;

    [SerializeField] GameObject RogueFairyUI;
    [SerializeField] Image[] spellIcons;
    [SerializeField] TextMeshProUGUI[] spellNames;
    [SerializeField] TextMeshProUGUI[] desc;

    [SerializeField] TextMeshProUGUI NPCPhrase;

    [SerializeField] InventoryInput inventoryInput;

    bool isStoreOpen;
    public bool IsStoreOpen { get => isStoreOpen; set => isStoreOpen = value; }

    int[] SelectRandomSpellsForPlayer()
    {
        int[] spellIDsPicks = new int[3] { -1, -1, -1 };

        for (int i = 0; i < spellIDsPicks.Length; i++)
        {
            int spellIDPicked = 0;
            bool notRepeated = false;

            while (!notRepeated)
            {
                spellIDPicked = Random.Range(0, spells.Length);

                for (int j = 0; j < spellIDsPicks.Length; j++)
                {
                    if (spellIDPicked == spellIDsPicks[j])
                    {
                        notRepeated = false;
                        break;
                    }
                    else notRepeated = true;
                }
            }

            spellIDsPicks[i] = spellIDPicked;
        }

        return spellIDsPicks;
    }

    public void Buy(int index)
    {
        ManagerHechizos.instance.AddNewSpell(spells[index]);
    }

    public void OpenStore()
    {
        isStoreOpen = true;
        RogueFairyUI.SetActive(true);

        string[] phrases = new string[]
        {
            "Normalmente te daría uno solo, pero siento que aún está muy temprano, asi que toma estos 3 hechizos",
            "Te prestaré estos 3 hechizos, con esto deberías poder avanzar!",
            "Toma estos 3 hechizos, luego vendrán cosas mejores ;)"
        };

        NPCPhrase.text = phrases[Random.Range(0, phrases.Length)];

        int[] pickedSpells = SelectRandomSpellsForPlayer();

        for (int i = 0; i < pickedSpells.Length; i++)
        {
            ManagerHechizos.instance.AddNewSpell(spells[pickedSpells[i]]);

            spellIcons[i].sprite = spells[pickedSpells[i]].sprite;
            spellNames[i].text = spells[pickedSpells[i]].spellName;
            desc[i].text = spells[pickedSpells[i]].desc;
        }

        GetComponent<SphereCollider>().enabled = false;

        inventoryInput.ActiveCursor(true);
        Inventory.instance.TimeChange(true);
    }

    public void CloseStore()
    {
        isStoreOpen = false;
        RogueFairyUI.SetActive(false);

        inventoryInput.ActiveCursor(false);
        Inventory.instance.TimeChange(false);
    }
}
