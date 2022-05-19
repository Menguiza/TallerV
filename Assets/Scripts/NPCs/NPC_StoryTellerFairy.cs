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
        initialPhrase = "Soy una de las hadas m�s viejas y sabias en el reino. Se muchas cosas de nuestro pasado y cultura.";
        dialogText.text = initialPhrase;

        audioSource = GetComponent<AudioSource>();

        phrases = new string[9]
        {
            "Hace muchos tiempo antes de que existiera el reino de N�fera, exist�an varios clanes que se peleaban todo el tiempo por la supremac�a.",
            "Las hadas on�ricas hemos estado con los humanos desde hace milenios, necesitamos sus sue�os para producir magia on�rica y los humanos necesitan de nosotras para obtener la magia creada.",
            "Hace a�os subi� al poder el rey malvado Desmos el cual con ayuda del hada on�rica malvada Gos invocaron un hechizo para privar de los habitantes del reino la capacidad de dormir y as� obtener el poder absoluto.",
            "Recuerdo que Gos el hada malvada no hablaba mucho con las otras hadas y siempre nos miraba con desprecio.",
            "Hemos tratado de encontrar un h�roe c�mo t� desde hace mucho tiempo, menos mal que por fin te encontramos.",
            "S�lo t� puedes ayudarnos porque por ser de otro mundo el hechizo de anulaci�n de sue�o de Desmos no te afecta y al so�ar puedes obtener nuestra magia.",
            "Hace ya varios milenios se cre� el reino de N�fera cuando el primer rey mago uni� a todos los clanes m�gicos que antes luchaban entre s�.",
            "La resistencia se cre� para intentar acabar con Desmos, hace ya varios a�os.",
            "Todos los hechizos y posturas vienen de los diferentes clanes m�gicos que exist�an hace a�os antes que se crear� el reino de N�fera, cada uno ten�a situaciones y caracter�sticas diferentes con las cuales basaron sus hechizos y posturas.",
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
