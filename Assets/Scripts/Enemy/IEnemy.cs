using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public int Damage { get; }
    public int Conciencia { get; }

    void ReceiveDamage(int dmg);

    void DestroyEnemy();
}
