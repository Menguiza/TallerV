using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int index;

    private void Start()
    {
        RoomManager.instance.onChangeScene.AddListener(CanChange);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Invoke("ChangeScene", 0.5f);
        }
    }

    void ChangeScene()
    {
        RoomManager.instance.onChangeScene?.Invoke();
    }

    void CanChange()
    {
        if(transform.childCount<=0)
        {
            SceneManager.LoadScene(index);
        }
    }
}
