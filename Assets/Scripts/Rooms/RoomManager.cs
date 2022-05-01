using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

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
        foreach (RoomSpawners element in room.data)
        {
            Instantiate(element.objects[RandomInt(element.objects.Count - 1)], element.spawns[RandomInt(element.spawns.Count - 1)], Quaternion.identity);
            
        }
    }

    void SetUpRoom(Room room, EnemyController enemy)
    {
        foreach (RoomSpawners element in room.data)
        {
            foreach(GameObject obj in element.objects)
            {
                if(obj.GetComponent<EnemyController>() == enemy)
                {
                    Instantiate(obj, element.spawns[RandomInt(element.spawns.Count - 1)], Quaternion.identity);
                }
            }
        }
    }

    Room SelectRoom()
    {
        return rooms[RandomInt(rooms.Count - 1)];
    }

    int RandomInt(int max)
    {
        int rnd = Random.Range(0, max);
        return rnd;
    }
}
