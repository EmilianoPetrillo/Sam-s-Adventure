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
    public Button skipButton;
    public float TimePerCharacter = 0.1f;

    // Start is called before the first frame update
    void Start()
    { 
    }

    public void StartShowSlides()
    {
        slidesText = new List<string>();
        AddSlides();
        StartCoroutine(ShowSlides());
    }

    private void AddSlides()
    {
        slidesText.Add("THOUSANDS OF YEARS AGO \nTHE DEVORER OF UNIVERSES, A DIVINITY OF INFINITE POWERS, ENDED ENTIRE UNIVERSES, GENERATING CHAOS AND DESTRUCTION.");
        slidesText.Add("UNTIL A HERO SACRIFIES HIMSELF TO TRAP THE CREATURE INSIDE A MAGIC BOX, ENDING HIS ACTIONS.");
        slidesText.Add("TODAY \nTHROUGH THE USE OF BLACK MAGIC, THE FOLLOWING PRIEST OF THE DEVORATOR RELEASES HIM FROM THE CHAINS THAT IMPRISON.");
        slidesText.Add("PLANET EARTH \nSAM IS A BOY LIKE MANY WHO LIVES ON PLANET EARTH, GOES TO SCHOOL AND LIVES WITH HIS MOTHER AND SISTER.");
        slidesText.Add("ONE NIGHT SAM IS TELETRANSPORTED TO THE OBSERVATORS SPACE STATION.");
        slidesText.Add("HERE HE MEETS THE OBSERVATORS, HEAVENLY ENTITIES WHO HAVE THE POWER TO SEE IN EVERY CORNER OF THE UNIVERSE.");
        slidesText.Add("HERE HE IS MADE TO KNOW THE PRESENCE OF THE DEVORATOR, HE IS MADE TO KNOW THAT HE IS THE ONLY ONE ABLE TO DEFEAT THE MONSTER, AS LIST OF THE LEGENDARY HERO WHO MILLEN YEARS PRIOR SACRIFIES HIMSELF TO SAVE ALL UNIVERSES.");
        slidesText.Add("IN ORDER TO DO THIS, HE MUST FIRST RECOVER 3 PIECES OF A MAGIC BOX, PRESENT IN 3 DIFFERENT UNIVERSES, IN WHICH THE DIVORATOR HAS LEADED THE SOULS OF THE PLACE TO BE EVIL AND DANGEROUS MONSTERS.");
        slidesText.Add("OBSERVATORS PROVIDE SAM WITH THE WEAPONS NEEDED TO BEGIN HIS ADVENTURE.");
        slidesText.Add("THE PATH THAT WILL TAKE SAM A VERY LONG JOURNEY BEGINS, IN SEARCH OF THE OBJECTS NEEDED TO FACE THE FEARED DEVORATOR OF UNIVERSE!");


    }

    IEnumerator ShowSlides()
    {
        for(int i = 0; i < slidesText.Count; i++)
        {
            continueButton.gameObject.SetActive(false);
            yield return StartCoroutine(ShowText(slidesText[i]));
        }
        GameObject.Find("LoadController").GetComponent<LoadController>().LoadGameScene();
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
        skipButton.gameObject.SetActive(true);
        var waitForButton = new WaitForUIButtons(continueButton);
        yield return waitForButton.Reset();
    }

    public void SkipIntro()
    {
        GameObject.Find("LoadController").GetComponent<LoadController>().LoadGameScene();
    }


}
