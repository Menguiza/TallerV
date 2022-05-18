using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_FlechaDeFuego : MonoBehaviour
{
    public float damage;

    [SerializeField] GameObject fireArrowExplotion;

    public void fireArrowImpact()
    {
        //Aqui van las particulas --
        GameObject instance = Instantiate(fireArrowExplotion, transform.position, Quaternion.identity);
        Destroy(instance, 4f);

        //Siempre Eliminar al final el proyectil
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<IEnemy>() != null)
        {
            int spellDamage = GameMaster.instance.CalculateSpellDamage(damage);

            collider.gameObject.GetComponent<IEnemy>().ReceiveDamage(spellDamage);
            GameObject popUpInstace = Instantiate(GameMaster.instance.DamagePopUp, collider.transform.position + Vector3.up * 0.5f + Vector3.right, GameMaster.instance.DamagePopUp.transform.rotation);
            popUpInstace.GetComponent<DamagePopUp>().SetText(AttackType.normal, spellDamage);
        }

        fireArrowImpact();
    }
}
