using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "nuevoHechizo", menuName = "ScriptableObjects/Hechizo", order = 2)]
public class Hechizo : ScriptableObject
{
    public enum EHechizo
    {
        BolaDeFuego = 0,
        FlechaDeFuego = 1,
        EspadaDeLuz = 2,
        PedradaMagica = 3,
        BoomerangDeEnergia = 4,
        BolaDeAcido = 5,
        DashElectrico = 6,
        NA = -1
    }

    public Sprite sprite;
    public string spellName;
    public string desc;
    public string lore;

    public int spellDamage;
    public string impact;
    public string impactEffect;
    public float rechargeTime;
    public float chanellingTime;
    public EHechizo spellContained;
}
