using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    public UnityEvent onChangeScene, onRoomFinished;

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

        if(rooms.Count !=0)
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
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            return rooms[0];
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            return rooms[1];
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            return rooms[2];
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            return rooms[3];
        }

        return null;
    }

    int RandomInt(int max)
    {
        int rnd = Random.Range(0, max);
        return rnd;
    }
}
