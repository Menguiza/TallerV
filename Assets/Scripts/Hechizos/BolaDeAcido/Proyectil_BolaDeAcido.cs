using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_BolaDeAcido : MonoBehaviour
{
    public float damage;

    [SerializeField] GameObject poissonExplotion;

    public void acidBallImpact()
    {
        //Aqui van las particulas --
        GameObject explotion = Instantiate(poissonExplotion, transform.localPosition, Quaternion.identity);
        Destroy(explotion, 4f);

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
        

        acidBallImpact();
    }
}
