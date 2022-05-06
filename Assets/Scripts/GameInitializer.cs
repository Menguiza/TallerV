using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Start()
    {
        Economy.instance.RewardGems((uint)GameData.RetreiveGems()); 
    }
}
