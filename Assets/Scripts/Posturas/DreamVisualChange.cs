using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class DreamVisualChange : MonoBehaviour
{
    Volume volume;

    [SerializeField] VolumeProfile wakeProfile;
    [SerializeField] VolumeProfile dreamProfile;
    [SerializeField] VolumeProfile nightmareProfile;

    private void Start()
    {
        volume = Camera.main.GetComponent<Volume>();

        GameMaster.instance.PlayerDream.AddListener(SetDreamProfile);
        GameMaster.instance.PlayerWake.AddListener(SetWakeProfile);
    }

    public void SetWakeProfile()
    {
        volume.profile = wakeProfile;
    }

    public void SetDreamProfile()
    {
        if (GameMaster.instance.Player.Pesadilla) volume.profile = nightmareProfile;
        else volume.profile = dreamProfile;
    }
}
