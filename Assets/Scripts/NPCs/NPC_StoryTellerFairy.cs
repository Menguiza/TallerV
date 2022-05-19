using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class NPC_StoryTellerFairy : MonoBehaviour, IVendorNPC, IInteractive
{
    string[] phrases;
    string initialPhrase;

    bool isStoreOpen;
    public bool IsStoreOpen { get => isStoreOpen; set => isStoreOpen = value; }

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] int textRange;

    [SerializeField] AudioClip talkSound;
    AudioSource audioSource;

    private void Start()
    {
        initialPhrase = "Soy una de las hadas más viejas y sabias en el reino. Se muchas cosas de nuestro pasado y cultura.";
        dialogText.text = initialPhrase;

        audioSource = GetComponent<AudioSource>();

        phrases = new string[9]
        {
            "Hace muchos tiempo antes de que existiera el reino de Nífera, existían varios clanes que se peleaban todo el tiempo por la supremacía.",
            "Las hadas oníricas hemos estado con los humanos desde hace milenios, necesitamos sus sueños para producir magia onírica y los humanos necesitan de nosotras para obtener la magia creada.",
            "Hace años subió al poder el rey malvado Desmos el cual con ayuda del hada onírica malvada Gos invocaron un hechizo para privar de los habitantes del reino la capacidad de dormir y así obtener el poder absoluto.",
            "Recuerdo que Gos el hada malvada no hablaba mucho con las otras hadas y siempre nos miraba con desprecio.",
            "Hemos tratado de encontrar un héroe cómo tú desde hace mucho tiempo, menos mal que por fin te encontramos.",
            "Sólo tú puedes ayudarnos porque por ser de otro mundo el hechizo de anulación de sueño de Desmos no te afecta y al soñar puedes obtener nuestra magia.",
            "Hace ya varios milenios se creó el reino de Nífera cuando el primer rey mago unió a todos los clanes mágicos que antes luchaban entre sí.",
            "La resistencia se creó para intentar acabar con Desmos, hace ya varios años.",
            "Todos los hechizos y posturas vienen de los diferentes clanes mágicos que existían hace años antes que se creará el reino de Nífera, cada uno tenía situaciones y características diferentes con las cuales basaron sus hechizos y posturas.",
        };
    }

    void Update()
    {
        float deltaAlpha;
        float distanceFromPlayer = Vector3.Distance(transform.position, GameMaster.instance.playerObject.transform.position);

        if (distanceFromPlayer > textRange) deltaAlpha = -Time.deltaTime;
        else deltaAlpha = Time.deltaTime;

        deltaAlpha = (float)Math.Round(deltaAlpha, 3);
        float newAlpha = Mathf.Clamp(image.color.a + deltaAlpha, 0, 1);

        image.color = new Color(1, 1, 1, newAlpha);
        dialogText.color = new Color(0, 0, 0, newAlpha);
    }

    public void Buy(int itemIndex)
    {
        // No deberia pasar
        throw new System.NotImplementedException();
    }

    public void CloseStore()
    {
        // No deberia pasar
        throw new System.NotImplementedException();
    }

    // It's actually interact
    public void OpenStore()
    {
        dialogText.text = phrases[UnityEngine.Random.Range(0, phrases.Length)];
        audioSource.PlayOneShot(talkSound);
    }
}
