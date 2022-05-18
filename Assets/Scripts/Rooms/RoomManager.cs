using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    public UnityEvent onChangeScene;
    public GameObject rewardPopUp;
    int[] indexes = new int[5];
    int count = 0;

    [SerializeField]
    List<Room> rooms = new List<Room>();
    [SerializeField]
    List<RewardInfo> rewards = new List<RewardInfo>();

    #region "Inputs Editor"

    [HideInInspector]
    public List<GameObject> objetoINP = new List<GameObject>(), spawnsINP = new List<GameObject>();

    #endregion

    private void Awake()
    {
        #region"Singleton"

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

    public void GenerateRandomRun()
    {
        count = 0;
        indexes = SetUpIndexes();
        SetUpRoom(SelectRoom());
        FindObjectOfType<SceneChanger>().index = indexes[count];
        count++;
    }

    public void UpdateInfo()
    {
        if(count!=0)
        {
            SetUpRoom(SelectRoom());
            FindObjectOfType<SceneChanger>().index = indexes[count];
            count++;
        }
    }

    private void Start()
    {
        GameMaster.instance.OnRoomFinished.AddListener(AssignReward);
    }

    void SetUpRoom(Room room)
    {
        if(room != null)
        {
            foreach (RoomSpawners element in room.data)
            {
                GameObject selected = element.objects[RandomInt(element.objects.Count)];

                if (selected.GetComponent<IEnemy>() != null)
                {
                    GameObject enemy = Instantiate(selected, element.spawns[RandomInt(element.spawns.Count)], selected.transform.rotation);

                    enemy.transform.parent = FindObjectOfType<SceneChanger>().transform;
                }
                else
                {
                    GameObject temp = Instantiate(selected, element.spawns[RandomInt(element.spawns.Count)], selected.transform.rotation);
                }
            }
        }
    }

    Room SelectRoom()
    {
        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            return rooms[0];
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            return rooms[1];
        }
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            return rooms[2];
        }
        else if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            return rooms[3];
        }

        return null;
    }

    int[] SetUpIndexes()
    {
        int[] indexes = new int[5];
        List<int> randoms = RandomListInt(4, 3);

        for(int i = 0; i<3; i++)
        {
            switch(randoms[i])
            {
                case 0:
                    indexes[i] = 5;
                    break;
                case 1:
                    indexes[i] = 6;
                    break;
                case 2:
                    indexes[i] = 7;
                    break;
                case 3:
                    indexes[i] = 8;
                    break;
            }
        }

        indexes[3] = 10;
        indexes[4] = 9;

        return indexes;
    }

    int RandomInt(int max)
    {
        int rnd = Random.Range(0, max);
        return rnd;
    }

    List<int> RandomListInt(int max, int limit)
    {
        List<int> listNumbers = new List<int>(limit);
        int number;
        for (int i = 0; i < limit; i++)
        {
            do
            {
                number = Random.Range(0, max);
            } while (listNumbers.Contains(number));
            listNumbers.Add(number);
        }

        return listNumbers;
    }

    void AssignReward()
    {
        if(SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            GameObject popUp = Instantiate(rewardPopUp, GameMaster.instance.playerObject.transform.position, Quaternion.identity);
            popUp.GetComponent<PlaceHolders>().coins.text = rewards[count-2].coins.ToString();
            popUp.GetComponent<PlaceHolders>().gems.text = rewards[count-2].gems.ToString();
            popUp.GetComponent<PlaceHolders>().icon = rewards[count-2].Give();
            Destroy(popUp, 5f);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 9)
        {
            GameObject popUp = Instantiate(rewardPopUp, GameMaster.instance.playerObject.transform.position, Quaternion.identity);
            popUp.GetComponent<PlaceHolders>().coins.text = rewards[rewards.Count-1].coins.ToString();
            popUp.GetComponent<PlaceHolders>().gems.text = rewards[rewards.Count - 1].gems.ToString();
            popUp.GetComponent<PlaceHolders>().icon = rewards[rewards.Count - 1].Give();
            Destroy(popUp, 5f);
        }
    }
}
