using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField]
    List<Item> items_Pasivos = new List<Item>();
    public DreamCatcher dmrcatcher { get; private set; }

    [SerializeField]
    public GameObject content, prefab, dmrContainer;

    byte slots = 5, slotsUsados = 0;
    [SerializeField]
    public List<GameObject> activables = new List<GameObject>(5);
    public List<GameObject> activosInv = new List<GameObject>(5);

    public UnityEvent OnItemCollected;
    public UnityEvent OnItemChanged;

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
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Item item = activables[0].GetComponent<ItemContainer>().itemInfo;

            if (item == null)
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

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Item item = activables[1].GetComponent<ItemContainer>().itemInfo;

            if (item == null)
            {
                return;
            }

            if (item.use == ItemUse.Tiempo)
            {
                item.AddParameters(item.duration);
            }
            else
            {
                item.Impact();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Item item = activables[2].GetComponent<ItemContainer>().itemInfo;

            if (item == null)
            {
                return;
            }

            if (item.use == ItemUse.Tiempo)
            {
                item.AddParameters(item.duration);
            }
            else
            {
                item.Impact();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Item item = activables[3].GetComponent<ItemContainer>().itemInfo;

            if (item == null)
            {
                return;
            }

            if (item.use == ItemUse.Tiempo)
            {
                item.AddParameters(item.duration);
            }
            else
            {
                item.Impact();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Item item = activables[4].GetComponent<ItemContainer>().itemInfo;

            if (item == null)
            {
                return;
            }

            if (item.use == ItemUse.Tiempo)
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

    public void Remove(Item it)
    {
        foreach (GameObject item in activables)
        {
            if (item.GetComponent<ItemContainer>().itemInfo == it)
            {
                item.GetComponent<ItemContainer>().itemInfo = null;
                Debug.Log("removed ");
                break;
            }
        }

        foreach (GameObject item in activosInv)
        {
            if (item.GetComponent<ItemContainerInv>().itemInfo == it)
            {
                item.GetComponent<ItemContainerInv>().itemInfo = null;
                Debug.Log("removed Inv");
                break;
            }
        }
    }

    public void Remove()
    {
        dmrcatcher.ResetParameter();
        dmrcatcher = null;
        Debug.Log("removed dream");
    }

    public void AddDreamcatcher(DreamCatcher dreamSent)
    {
        dmrcatcher = dreamSent;
        Debug.Log("added dream");
    }

    public void AddDreamStats()
    {
        if(dmrcatcher != null)
        {
            dmrcatcher.AddParameters();
        }
    }

    public void RemoveDreamStats()
    {
        if (dmrcatcher != null)
        {
            dmrcatcher.ResetParameter();
        }
    }

    public void SlotLoad()
    {
        for (int i = 0; i < activables.Count; i++)
        {
            activosInv[i].GetComponent<ItemContainerInv>().itemInfo = activables[i].GetComponent<ItemContainer>().itemInfo;
        }
    }
    public void SlotLoadReverse()
    {
        for (int i = 0; i < activosInv.Count; i++)
        {
            activables[i].GetComponent<ItemContainer>().itemInfo = activosInv[i].GetComponent<ItemContainerInv>().itemInfo;
        }
    }

    public void TimeChange(bool toggle)
    {
        if(toggle)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    #region"Generación de Items"

    [Header("Item arrays")]
    [SerializeField] Item[] commonItems;
    [SerializeField] Item[] rareItems;
    [SerializeField] Item[] dreamerItems;
    [SerializeField] Item[] forgottenItems;


    public Item GenerateRandomItem()
    {
        Item item;
        int chanceNumber = GenerateRandomChance();

        if (chanceNumber < 40) item = GenerateRandomCommonItem();
        else if (chanceNumber < 70) item = GenerateRandomRareItem();
        else if (chanceNumber < 90) item = GenerateRandomDreamerItem();
        else item = GenerateRandomForgottenItem();

        return item;
    }

    Item GenerateRandomCommonItem()
    {
        Item item = commonItems[GenerateRandomArrayIndex(commonItems.Length)];
        return item;
    }

    Item GenerateRandomRareItem()
    {
        Item item = rareItems[GenerateRandomArrayIndex(rareItems.Length)];
        return item;
    }

    Item GenerateRandomDreamerItem()
    {
        Item item = dreamerItems[GenerateRandomArrayIndex(dreamerItems.Length)];     
        return item;
    }

    Item GenerateRandomForgottenItem()
    {
        Item item = forgottenItems[GenerateRandomArrayIndex(forgottenItems.Length)];       
        return item;
    }

    int GenerateRandomChance()
    {
        int randomChance = Random.Range(1, 101);
        return randomChance;
    }

    int GenerateRandomArrayIndex(int size)
    {
        int randomArrayIndex = Random.Range(0, size);
        return randomArrayIndex;
    }
    #endregion
}
