using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    public List<Sprite> slidesSprites;

    public Image centralImage;
    public Button continueButton;
    public Button skipButton;
    public Button backButton;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        StartCoroutine(ShowSlides());
    }

    private void DecreaseI()
    {
        if (i == 0) i--;
        if (i > 0) i-=2;
    }

    IEnumerator ShowSlides()
    {
        for (i = 0; i < slidesSprites.Count; i++)
        {
            skipButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            yield return StartCoroutine(ShowSlide(i));
        }
        SkipIntro();
    }

    IEnumerator ShowSlide(int i)
    {
        centralImage.sprite = slidesSprites[i];
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
        GameController.Instance.StartStuff();
        transform.gameObject.SetActive(false);

    }


}
