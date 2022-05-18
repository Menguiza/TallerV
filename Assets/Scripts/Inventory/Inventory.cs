using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField]
    List<Item> items_Pasivos = new List<Item>();
    Item[] items_Activos = new Item[5];
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

        OnItemCollected.AddListener(SlotLoad);
        OnItemChanged.AddListener(SlotLoadReverse);

        for(int i = 0; i<5; i++)
        {
            items_Activos[i] = null;
        }
    }

    private void Update()
    {
        if(GameMaster.instance.Player.Life>0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Item item = activables[0].GetComponent<ItemContainer>().itemInfo;

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
    }

    public void AddItem(Item item)
    {
        if ((item.type == ItemType.Activo || item.type == ItemType.Tiempo) && slotsUsados < slots)
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
                                items_Pasivos.Add(item);
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

    public void StartShit(float secs, Item it)
    {
        StartCoroutine(it.Delete(secs));
    }

    public void Remove(Item it)
    {
        int count = 0;

        foreach (GameObject item in activables)
        {
            if (item.GetComponent<ItemContainer>().itemInfo == it)
            {
                item.GetComponent<ItemContainer>().itemInfo = null;

                items_Activos[count] = null;

                Debug.Log("removed ");
                break;
            }

            count++;
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
            items_Activos[i] = activables[i].GetComponent<ItemContainer>().itemInfo;
        }
    }
    public void SlotLoadReverse()
    {
        for (int i = 0; i < activosInv.Count; i++)
        {
            activables[i].GetComponent<ItemContainer>().itemInfo = activosInv[i].GetComponent<ItemContainerInv>().itemInfo;
            items_Activos[i] = activosInv[i].GetComponent<ItemContainerInv>().itemInfo;
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

    public void UpdateUi()
    {
        foreach (Item item in items_Pasivos)
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
                                return;
                            }
                        }
                    }
                }
            }

            GameObject pasivo = Instantiate(prefab, content.transform);
            pasivo.GetComponent<ItemContainer>().itemInfo = item;

            for (int i = 0; i < activosInv.Count; i++)
            {
                activables[i].GetComponent<ItemContainer>().itemInfo = items_Activos[i];
            }
        }
    }

    #region"Generación de Items"

    [Header("Passive Item arrays")]
    [SerializeField] Item[] commonPassiveItems;
    [SerializeField] Item[] rarePassiveItems;
    [SerializeField] Item[] dreamerPassiveItems;
    [SerializeField] Item[] forgottenPassiveItems;

    [Header("Active Item arrays")]
    [SerializeField] Item[] commonActiveItems;
    [SerializeField] Item[] rareActiveItems;
    [SerializeField] Item[] dreamerActiveItems;
    [SerializeField] Item[] forgottenActiveItems;

    public Item GenerateRandomItem(ItemType type)
    {
        Item item;
        int chanceNumber = GenerateRandomChance();

        if (chanceNumber < 40) item = GenerateRandomCommonItem(type);
        else if (chanceNumber < 70) item = GenerateRandomRareItem(type);
        else if (chanceNumber < 90) item = GenerateRandomDreamerItem(type);
        else item = GenerateRandomForgottenItem(type);

        print(item.nombre);

        return item;
    }

    Item GenerateRandomCommonItem(ItemType type)
    {
        Item item;

        if (type == ItemType.Pasivo) item = commonPassiveItems[GenerateRandomArrayIndex(commonPassiveItems.Length)]; 
        else item = commonActiveItems[GenerateRandomArrayIndex(commonActiveItems.Length)];

        return item;
    }

    Item GenerateRandomRareItem(ItemType type)
    {
        Item item;

        if (type == ItemType.Pasivo) item = rarePassiveItems[GenerateRandomArrayIndex(rarePassiveItems.Length)];
        else item = rareActiveItems[GenerateRandomArrayIndex(rareActiveItems.Length)];

        return item;
    }

    Item GenerateRandomDreamerItem(ItemType type)
    {
        Item item;
        
        if (type == ItemType.Pasivo) item = dreamerPassiveItems[GenerateRandomArrayIndex(dreamerPassiveItems.Length)];
        else item = dreamerActiveItems[GenerateRandomArrayIndex(dreamerActiveItems.Length)];

        return item;
    }

    Item GenerateRandomForgottenItem(ItemType type)
    {
        Item item;
        
        if (type == ItemType.Pasivo) item = forgottenPassiveItems[GenerateRandomArrayIndex(forgottenPassiveItems.Length)];
        else item = forgottenActiveItems[GenerateRandomArrayIndex(forgottenActiveItems.Length)];

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
