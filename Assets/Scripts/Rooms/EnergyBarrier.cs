using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarrier : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particles;
    BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void DestroyBarrier()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Stop();
        }

        boxCollider.enabled = false;
        Destroy(gameObject, 5f);
    }
}
