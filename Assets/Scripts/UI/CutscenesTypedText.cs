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
    public Button backButton;
    public int whatSlideToUse = 1;
    public float TimePerCharacter = 0.1f;
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void DecreaseI()
    {
        if (i == 0) i--;
        if (i > 0) i -= 2;
    }


    public void StartShowSlides()
    {
        i = 0;
        slidesText = new List<string>();
        AddSlides();
        StartCoroutine(ShowSlides());
    }

    private void AddSlides()
    {
        if (whatSlideToUse == 1)
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
        if (whatSlideToUse == 2)
        {
            slidesText.Add("AFTER DEFEATING THE GUARDIAN FROST, SAM IS CALLED TO THE OBSERVATORS.");
            slidesText.Add("OBSERVATORS GIVE SAM A WEAPON TO DEFEAT THE DEVORER OF UNIVERSES. THIS WEAPON IS CALLED: LAST BREATHE!");
            slidesText.Add("SAM LEAVES AT THE SPACE, THE LAST UNIVERSE, HOME OF THE MONSTER.");

        }
        if (whatSlideToUse == 3)
        {
            slidesText.Add("SAM MANAGES TO DEFEAT THE DEVOURER BY PERMANENTLY TRAPPING HIM IN THE MAGIC CUBE.");
            slidesText.Add("SAM BRINGS THE MAGICAL CUBE TO THE OBSERVERS, WHO CONSECRATE HIM AS THE ETERNAL HERO.");
            slidesText.Add("SAM RETURNS TO HIS NORMAL LIFE, AS IF NOTHING HAD EVER HAPPENED.");

        }
    }

    IEnumerator ShowSlides()
    {
        for(i = 0; i < slidesText.Count; i++)
        {
            skipButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            yield return StartCoroutine(ShowText(slidesText[i]));
        }
        SkipIntro();
    }

    IEnumerator ShowText(string phrase)
    {
        StringBuilder sb = new StringBuilder();
        for (int j = 1; j <= phrase.Length; j++)
        {
            sb.Append(phrase.Substring(j - 1, 1));
            Text.text = sb.ToString();

            yield return new WaitForSeconds(TimePerCharacter);
        }
        continueButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        var waitForButton = new WaitForUIButtons(continueButton, backButton);
        yield return waitForButton.Reset();
        if (waitForButton.PressedButton == backButton)
        {
            DecreaseI();
        }
    }

    public void SkipIntro()
    {
        if (whatSlideToUse == 1)
        {
            GameObject.Find("LoadController").GetComponent<LoadController>().LoadGameScene();
        }
        if (whatSlideToUse == 2)
        {
            UIController.Instance.FinishedCutsceneBefore4World();
        }
        if (whatSlideToUse == 3)
        {
            UIController.Instance.FinishedCutsceneDevourerKilled();
        }


    }


}
