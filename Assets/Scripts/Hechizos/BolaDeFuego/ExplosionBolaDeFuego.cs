using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBolaDeFuego : MonoBehaviour
{
    public float damage;

    [SerializeField] GameObject fireballExplotion;

    private void Awake()
    {
        Destroy(gameObject, 0.05f);

        fireballExplotion.transform.SetParent(null);
        Destroy(fireballExplotion, 4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IEnemy>() != null)
        {
            int spellDamage = GameMaster.instance.CalculateSpellDamage(damage);

            other.gameObject.GetComponent<IEnemy>().ReceiveDamage(spellDamage);
            GameObject popUpInstace = Instantiate(GameMaster.instance.DamagePopUp, other.transform.position + Vector3.up * 0.5f + Vector3.right, GameMaster.instance.DamagePopUp.transform.rotation);
            popUpInstace.GetComponent<DamagePopUp>().SetText(AttackType.normal, spellDamage);
        }
    }
}
