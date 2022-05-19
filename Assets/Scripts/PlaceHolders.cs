using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class PlaceHolders : MonoBehaviour
{
    public Sprite icon = null;

    public TMP_Text coins, gems;

    [SerializeField] Image img;

    // Update is called once per frame
    void Update()
    {
        img.sprite = icon;

        transform.position = GameMaster.instance.playerObject.transform.position;
    }
}
