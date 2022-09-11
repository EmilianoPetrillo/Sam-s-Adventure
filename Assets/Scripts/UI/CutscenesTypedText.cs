using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.UI;

public class CutscenesTypedText : MonoBehaviour
{
    private List<string> slidesText;

    public TextMeshProUGUI Text;
    public Button continueButton;
    public float TimePerCharacter = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        slidesText = new List<string>();
        AddSlides();
        StartCoroutine(ShowSlides());
    }

    private void AddSlides()
    {
        slidesText.Add("MIGLIAIA DI ANNI FA\nIL DIVORATORE DI UNIVERSI, UNA DIVINITA' DAI POTERI INFINITI, PONEVA FINE AD INTERI UNIVERSI, GENERANDO CAOS E DISTRUZIONE.");
        slidesText.Add("FINO A QUANDO UN EROE SACRIFICO' SE STESSO PER INTRAPPOLARE LA CREATURA ALL'INTERNO DI UNA SCATOLA MAGICA, PONENDO FINE ALLE SUE AZIONI.");
        slidesText.Add("OGGI\nTRAMITE L'UTILIZZO DELLA MAGIA NERA, IL PRETE SEGUACE DEL DIVORATORE LO LIBERA DALLE CATENE CHE IMPRIOGIONANO.");
        slidesText.Add("PIANETA TERRA\nSAM E' UN RAGAZZO COME TANTI CHE VIVE SUL PIANETA TERRA, VA A SCUOLA E VIVE CON LA MADRE E LA SORELLA.");
        slidesText.Add("UNA NOTTE VIENE TELETRASPORTATO PRESSO LA STAZIONE SPAZIALE DEGLI OSSERVATORI.");
        slidesText.Add("QUI CONOSCE GLI OSSERVATORI, ENTITA' CELESTIALI CHE HANNO IL POTERE DI VEDERE IN OGNI ANGOLO DELL'UNIVERSO.");
        slidesText.Add("QUI VIENE MESSO A CONOSCENZA DELLA PRESENZA DEL DIVORATORE, GLI VIENE FATTO SAPERE DI ESSERE L'UNICO IN GRADO DI SCONFIGGERE IL MOSTRO, POICHE' DISCENTENTE DELL'EROE LEGGENDARIO CHE MILLENNI PRIMA SACRIFICO' SE STESSO PER SALVARE TUTTI GLI UNIVERSI.");
        slidesText.Add("PER FARLO PERO' DOVRA' PRIMA RECUPERARE 3 PEZZI DI UNA SCATOLA MAGICA, PRESENTI IN 3 UNIVERSI DIFFERENTI, IN CUI IL DIVORATORE HA PORTATO LE ANIME DEL LUOGO AD ESSERE DEI MOSTRI MALEFICI E PERICOLOSI.");
        slidesText.Add("GLI OSSERVATORI FORNISCONO A SAM LE ARMI NECESSARIE PER INIZIARE LA SUA AVVENTURA.");
        slidesText.Add("PARTE COSI IL PERCORSO CHE PORTERA' SAM A PERCORRERE UN LUNGHISSIMO VIAGGIO ALLA RICERCA DEGLI OGGETTI NECESSARI PER AFFRONTARE IL TEMIBILE DIVORATORE DI UNIVERSI!");


    }

    IEnumerator ShowSlides()
    {
        for(int i = 0; i < slidesText.Count; i++)
        {
            Debug.Log("Showing Slide: "+i);
            continueButton.gameObject.SetActive(false);
            yield return StartCoroutine(ShowText(slidesText[i]));
        }
        Debug.Log("Load Game");
    }

        IEnumerator ShowText(string phrase)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 1; i <= phrase.Length; i++)
        {
            sb.Append(phrase.Substring(i - 1, 1));
            Text.text = sb.ToString();

            yield return new WaitForSeconds(TimePerCharacter);
        }
        continueButton.gameObject.SetActive(true);
        var waitForButton = new WaitForUIButtons(continueButton);
        yield return waitForButton.Reset();
    }

}
