using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ConseguirHechizo : MonoBehaviour
{
    public Hechizo spellContained;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            ManagerHechizos.instance.AddNewSpell(spellContained);
            ManagerHechizos.instance.UpdateSpellSlots();
        }
    }
}
