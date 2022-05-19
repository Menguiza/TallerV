using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int index;
    [SerializeField] bool markedAsInitializer, markedAsLoader, markedAsLobbyExit;
    [SerializeField] bool markedAsBoss;

    private void Start()
    {
        RoomManager.instance.onChangeScene.AddListener(CanChange);
        if (markedAsInitializer) RoomManager.instance.GenerateRandomRun();
        if (markedAsLoader)
        {
            if(SceneManager.GetActiveScene().buildIndex != 9)
            {
                RoomManager.instance.UpdateInfo();
            }

            if(RoomManager.instance.count > 2)
            {
                Inventory.instance.InvokeWithDelay();
            }
        }
        if (markedAsLobbyExit)
        {
            SoundManager.instance.SetLobbyMusic();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {            
            FadeScreen fs = FindObjectOfType<FadeScreen>();
            fs.StartCoroutine(fs.FadeBlack());
           if(markedAsBoss) GameMaster.instance.OnRunEnd?.Invoke();
            Invoke("ChangeScene", 0.5f);
        }
    }

    void ChangeScene()
    {
        RoomManager.instance.onChangeScene?.Invoke();
        if (markedAsLobbyExit)
        {
            SoundManager.instance.SetForestMusic();
        }
    }

    void CanChange()
    {
        if(transform.childCount<=0)
        {
            SceneManager.LoadScene(index);
        }
    }
}
