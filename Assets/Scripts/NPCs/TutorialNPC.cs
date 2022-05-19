using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialNPC : MonoBehaviour, IInteractive, IVendorNPC
{
    public int stage;

    bool isStoreOpen;
    public bool IsStoreOpen { get => isStoreOpen; set => isStoreOpen = value; }

    public TextMeshProUGUI textNPC;
    string[] phrases;

    [SerializeField] GameObject tutSection1;
    [SerializeField] GameObject tutSection2;
    [SerializeField] GameObject tutSection3;
    [SerializeField] GameObject tutSection4;
    [SerializeField] GameObject tutSection5;
    [SerializeField] GameObject tutSection6;
    [SerializeField] GameObject tutSection7;

    void Start()
    {
        textNPC.text = "Llegaste! Oh poderoso humano, soy Nerva! necesitamos tu ayuda\nUsa 'F' para interatuar!";

        phrases = new string[9]
        {
            "Avanza humano! te guiaré hasta nuestro pueblo", // S0 Intro 
            "Para moverte en este mundo, usa 'A' 'D' y 'Espacio' para moverte hacia los lados y saltar", // S1 Moverse y saltar
            "Usa 'Shift' para rodar, puede ayudarte a desplazarte pero NO te protegerá del daño", // S2 Dash
            "Toma esta bola de fuego, usala con 'Q', apunta con el 'Mouse' y lánzala a esa caja!\nCuando tengas más hechizos los podrás usar con 'Q, W, E'", // S3 Hechizos
            "Oh, conseguiste un objeto activo! Usalo con '1'\n Cuando tengas más objetos activos los´podrás usar con '1, 2, 3, 4, 5'", // S4 Objetos
            "Pégale a esa odiosa caja usando 'Click izquierdo'! ", // S5 Pegar
            "Usa 'Tab' para abrir tu inventario y ver tus estadísticas", // S6 Inventario
            "Debes derrotar a todos los enemigos de la sala para salir de esta, derrota a este slime!", // S7 Objetivo
            "Por último, usa 'Escape' para abrir la pausa si en algún momento necesitas respirar, ajustar o ver los controles" // S8 Final
        };
    }

    public void OpenStore()
    {
        switch (stage)
        {
            case 0:
                textNPC.text = phrases[stage];
                break;
            case 1:
                textNPC.text = phrases[stage];
                break;
            case 2:
                textNPC.text = phrases[stage];
                break;
            case 3:
                textNPC.text = phrases[stage];
                break;
            case 4:
                textNPC.text = phrases[stage];
                break;
            case 5:
                textNPC.text = phrases[stage];
                break;
            case 6:
                textNPC.text = phrases[stage];
                break;
            case 7:
                textNPC.text = phrases[stage];
                break;
            case 8:
                textNPC.text = phrases[stage];
                break;
        }
    }

    #region"Tutorial methods"
   

    public void SetStage1()
    {
        textNPC.text = "Para moverte en este mundo, usa 'A' 'D' y 'Espacio' para moverte hacia los lados y saltar";
        tutSection1.SetActive(true);
    }

    public void SetStage2()
    {
        textNPC.text = "Usa 'Shift' para rodar, puede ayudarte a desplazarte pero NO te protegerá del daño"; // S2 Dash
        tutSection2.SetActive(true);
    }

    [SerializeField] Hechizo hechizo;
    public void SetStage3()
    {
        textNPC.text = "Toma esta bola de fuego, usala con 'Q', apunta con el 'Mouse' y lánzala a esa caja!\nCuando tengas más hechizos los podrás usar con 'Q, W, E'";
        ManagerHechizos.instance.AddNewSpell(hechizo);
        tutSection3.SetActive(true);
    }

    public void SetStage4()
    {
        textNPC.text = "Oh, conseguiste un objeto activo! Usalo con '1'\n Cuando tengas más objetos activos los´podrás usar con '1, 2, 3, 4, 5'";
    }

    public void SetStage5()
    {
        textNPC.text = "Al quedar insconciente empiezas a soñar y eres más poderoso! Tu consciencia se irá recuperando con el tiempo. Ahora Pégale a esa odiosa caja usando 'Click izquierdo'! ";
        tutSection4.SetActive(true);
    }

    public void SetStage6()
    {
        textNPC.text = "Usa 'Tab' para abrir tu inventario y ver tus estadísticas";
    }
    public void SetStage7()
    {
        textNPC.text = "Debes derrotar a todos los enemigos de la sala para salir de esta, derrota a este slime!";
        tutSection6.SetActive(true);
    }

    #endregion

    public void CloseStore()
    {

    }
    public void Buy(int i)
    {

    }
}
