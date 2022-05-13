using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UpdateTimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] displayTimers;

    private void Start()
    {
        for (int i = 0; i < displayTimers.Length; i++)
        {
            displayTimers[i].text = "";
        }
    }

    private void Update()
    {
        for (int i = 0; i < displayTimers.Length; i++)
        {
            if (ManagerHechizos.instance.spellsData[i] != null)
            {
                float timeAmount = (float)Math.Round((ManagerHechizos.instance.availableSpells[i] as IHechizo).RemainingCD, 1);
                displayTimers[i].text = timeAmount.ToString();
                if (timeAmount <= 0)
                {
                    displayTimers[i].text = "";
                }
                
            }
        }
    }
}
