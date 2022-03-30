using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    bool called = false;
    [SerializeField]
    float delay = 3f;

    void Update()
    {
        if (GetComponentInChildren<EnemyController>() == null && !called)
        {
            called = true;
            Invoke("Spawn", delay);
        }
    }

    void Spawn()
    {
        GameObject enemy = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        enemy.transform.parent = transform;

        called = false;
    }
}
