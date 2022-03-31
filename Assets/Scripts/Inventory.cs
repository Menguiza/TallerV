using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField]
    List<Item> items_Pasivos = new List<Item>();

    public List<Item> items_Activos = new List<Item>();
    [SerializeField]
    GameObject content, prefab;
    byte slots = 5, slotsUsados = 0;
    [SerializeField]
    List<GameObject> activables = new List<GameObject>(5);

    private void Awake()
    {
        #region "Singleton"

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            ScriptableObject obj = activables[0].GetComponent<ItemContainer>().itemInfo;
            Item item = activables[0].GetComponent<ItemContainer>().itemInfo;

            if (obj == null)
            {
                return;
            }

            if(item.use == ItemUse.Tiempo)
            {
                item.AddParameters(item.duration);
            }
            else
            {
                item.Impact();
            }
        }
    }

    public void AddItem(Item item)
    {
        if (item.type == ItemType.Activo && slotsUsados < slots)
        {
            items_Activos.Add(item);
            slotsUsados++;

            for (int i = 0; i < activables.Count; i++)
            {
                ScriptableObject obj = activables[i].GetComponent<ItemContainer>().itemInfo;

                if (obj == null)
                {
                    activables[i].GetComponent<ItemContainer>().itemInfo = item;
                    return;
                }
            }
        }
        else if (item.type == ItemType.Pasivo)
        {
            bool stack = false;

            if (item.format == ItemFormat.Stackeable)
            {
                if (items_Pasivos.Count != 0)
                {
                    foreach (Item element in items_Pasivos)
                    {
                        if (element.nombre == item.nombre)
                        {
                            stack = true;
                        }
                    }

                    if (stack)
                    {
                        foreach (Transform child in content.transform)
                        {
                            if (child.GetComponent<ItemContainer>().itemInfo.nombre == item.nombre)
                            {
                                child.GetComponent<ItemContainer>().counter++;
                                item.AddParameters();
                                return;
                            }
                        }
                    }
                }
            }

            items_Pasivos.Add(item);
            item.AddParameters();

            GameObject pasivo = Instantiate(prefab, content.transform);
            pasivo.GetComponent<ItemContainer>().itemInfo = item;
        }
    }

    public void Remove(string nombre)
    {
        foreach (Item item in items_Activos)
        {
            if (item.nombre == nombre)
            {
                items_Activos.Remove(item);
                break;
            }
        }

        for (int i = 0; i < activables.Count; i++)
        {
            Item obj = activables[i].GetComponent<ItemContainer>().itemInfo;

            if (obj.nombre == nombre)
            {
                activables[i].GetComponent<ItemContainer>().itemInfo = null;
                return;
            }
        }
    }
}
