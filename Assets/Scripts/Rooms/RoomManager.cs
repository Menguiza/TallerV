using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    public UnityEvent onChangeScene;
    int[] indexes = new int[5];
    int count = 0;

    [SerializeField]
    List<Room> rooms = new List<Room>();

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

        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            count = 0;
            indexes = SetUpIndexes();

            FindObjectOfType<SceneChanger>().index = indexes[count];
            count++;
        }
        else
        {
            FindObjectOfType<SceneChanger>().index = indexes[count];
            count++;
        }

        if (rooms.Count !=0)
        {
            SetUpRoom(SelectRoom());
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
                    GameObject enemy = Instantiate(selected, element.spawns[RandomInt(element.spawns.Count)], Quaternion.identity);

                    enemy.transform.parent = FindObjectOfType<SceneChanger>().transform;
                }
                else
                {
                    GameObject temp = Instantiate(selected, element.spawns[RandomInt(element.spawns.Count)], Quaternion.identity);
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

        for(int i = 0; i<3; i++)
        {
            int rnd = RandomInt(4);

            switch(rnd)
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

        indexes[3] = 2;
        indexes[4] = 9;

        return indexes;
    }

    int RandomInt(int max)
    {
        int rnd = Random.Range(0, max);
        return rnd;
    }
}
