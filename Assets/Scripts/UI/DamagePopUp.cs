using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum AttackType
{
    normal = 0,
    critic = 1,
    heal = 2,
    conscience = 3
}

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] GameObject PopUpObject;
    [SerializeField] TextMeshProUGUI PopUp;

    private void Awake()
    {
        Destroy(PopUpObject, 1.05f);
    }

    private void Update()
    {
        PopUp.alpha = Mathf.Clamp(PopUp.alpha - Time.deltaTime, 0, 1);
        transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
        transform.Translate(Vector3.up * Time.deltaTime);
    }

    public void SetText(AttackType attackType, int amount)
    {
        PopUp.text = amount.ToString();

        switch(attackType)
        {
            case AttackType.normal:
                PopUp.color = new Color(1, 0, 0, 1);
                break;
                
            case AttackType.critic:
                PopUp.fontSize = 0.8f;
                PopUp.color = new Color(1, 0.8f, 0, 1);
                break;

            case AttackType.heal:
                PopUp.color = new Color(0, 1, 0, 1);
                break;

            case AttackType.conscience:
                PopUp.color = new Color(0.82f, 0.15f, 0.71f, 1);
                break;
        }
    }
}
