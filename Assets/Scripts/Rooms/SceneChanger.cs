using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int index;
    [SerializeField] bool markedAsInitializer, markedAsLoader;

    private void Start()
    {
        RoomManager.instance.onChangeScene.AddListener(CanChange);
        if (markedAsInitializer) RoomManager.instance.GenerateRandomRun();
        if (markedAsLoader)
        {
            RoomManager.instance.UpdateInfo();

            if(RoomManager.instance.count > 2)
            {
                Inventory.instance.UpdateUi();
                Inventory.instance.OnItemCollected?.Invoke();
            }     
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {            
            FadeScreen fs = FindObjectOfType<FadeScreen>();
            fs.StartCoroutine(fs.FadeBlack());
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
