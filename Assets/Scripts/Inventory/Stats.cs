using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    [SerializeField]
    List<TMP_Text> dataPrefb = new List<TMP_Text>(9);

    // Update is called once per frame
    void Update()
    {
        if(dataPrefb.Count>0)
        {
            UpdateData();
        }
    }

    void UpdateData()
    {
        dataPrefb[0].text = GameMaster.instance.Player.Life + "/" + GameMaster.instance.Player.MaxLife;
        dataPrefb[1].text = GameMaster.instance.Player.Damage.ToString();
        dataPrefb[2].text = GameMaster.instance.Player.TGPC.ToString();
        dataPrefb[3].text = GameMaster.instance.Player.CritProb + "%";
        dataPrefb[4].text = GameMaster.instance.Player.RoboVida + "%";
        dataPrefb[5].text = "x" + GameMaster.instance.Player.MultVelAtaque.ToString();
        dataPrefb[6].text = "x" + GameMaster.instance.Player.SpeedMult.ToString();
        dataPrefb[7].text = GameMaster.instance.Player.MultPesadilla + "%";
        dataPrefb[8].text = "x" + GameMaster.instance.Player.MultDañoRecibido.ToString();
        dataPrefb[9].text = "x" + GameMaster.instance.Player.MultHechizos.ToString();
    }
}
