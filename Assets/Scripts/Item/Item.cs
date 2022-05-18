using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemRarity
{
    common = 0,
    rare = 1,
    dreamer = 2,
    forgotten = 3
}


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    [Header("Info.")]

    public string nombre = "";
    public ItemType type;
    public ItemFormat format;
    public ItemUse use;
    public ItemRarity rarity;
    public Sprite icon;
    public int price;

    [Header("Parametros")]

    public sbyte multVidaMax = 0;
    public sbyte multDmg = 0;
    public float multConciencia = 0;
    public sbyte multTGPC = 0;
    public sbyte multCritProb = 0;
    public float multCrit = 0;
    public sbyte multRoboPer = 0;
    public float multVelAatque = 0;
    public float multSpeed = 0;
    public sbyte multPesadillaPer = 0;
    public float multDañoRecibido = 0;
    public float multHechizos = 0;

    [Header("Impacto")]

    public sbyte sumVida = 0;
    public sbyte sumConciencia = 0;
    public byte sumDinero = 0;

    [Header("Temporal")]
    public float duration = 0;

    public void AddParameters()
    {
        GameMaster.instance.AddMod(nombre, multVidaMax, multDmg, multConciencia, multTGPC, multCritProb, multCrit, multRoboPer, multVelAatque, multSpeed, multPesadillaPer, multDañoRecibido, multHechizos);
    }

    public void AddParameters(float duration)
    {
        GameMaster.instance.AddMod(nombre, multVidaMax, multDmg, multConciencia, multTGPC, multCritProb, multCrit, multRoboPer, multVelAatque, multSpeed, multPesadillaPer, multDañoRecibido, multHechizos);
        Inventory.instance.Remove(this);
        Inventory.instance.StartShit(duration, this);
    }

    void ResetParameter()
    {
        GameMaster.instance.RemoveMod(nombre);
        Inventory.instance.Remove(this);
    }

    public void Impact()
    {
        GameMaster.instance.Player.Life += (uint)Mathf.Round((GameMaster.instance.maxLife * sumVida)/ 100);
        GameMaster.instance.Player.Conciencia = (ushort)Mathf.Max(0, GameMaster.instance.Player.Conciencia + sumConciencia);
        if(sumConciencia!=0)
        {
            GameMaster.instance.playerObject.GetComponent<Animator>().SetTrigger("Knock");
        }

        if (sumVida != 0)
        {
            GameObject popUpInstace = Instantiate(GameMaster.instance.DamagePopUp, GameMaster.instance.playerObject.transform.position + Vector3.up * 1.5f, GameMaster.instance.DamagePopUp.transform.rotation);
            popUpInstace.GetComponent<DamagePopUp>().SetText(AttackType.heal, sumVida);
        }
        else if (sumConciencia != 0)
        {
            GameObject popUpInstace = Instantiate(GameMaster.instance.DamagePopUp, GameMaster.instance.playerObject.transform.position + Vector3.up * 1.5f, GameMaster.instance.DamagePopUp.transform.rotation);
            popUpInstace.GetComponent<DamagePopUp>().SetText(AttackType.conscience, -sumConciencia);
        }

        Economy.instance.RewardCurrency(sumDinero);
        Inventory.instance.Remove(this);
    }

    public IEnumerator Delete(float wait)
    {
        yield return new WaitForSeconds(wait);

        Debug.Log("Reset");
        ResetParameter();

        yield return null;
    }
}

public enum ItemType { Activo, Tiempo, Pasivo };

public enum ItemUse { Tiempo, Indefinido};

public enum ItemFormat { Stackeable, Unique };
