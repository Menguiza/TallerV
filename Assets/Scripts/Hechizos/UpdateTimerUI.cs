using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] displayTimers;

    private void Update()
    {
        for (int i = 0; i < displayTimers.Length; i++)
        {
            if (ManagerHechizos.instance.spellsData[i] != null)
            {

            }
        }
    }
}
