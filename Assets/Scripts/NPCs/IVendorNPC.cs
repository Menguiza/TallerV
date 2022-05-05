using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVendorNPC 
{
    public bool IsStoreOpen { get; }

    public void Buy(int itemIndex);
    public void OpenStore();
    public void CloseStore();
}
