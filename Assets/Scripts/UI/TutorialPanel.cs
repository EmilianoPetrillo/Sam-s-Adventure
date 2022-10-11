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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowSlides());
    }


    IEnumerator ShowSlides()
    {
        for (int i = 0; i < slidesSprites.Count; i++)
        {
            continueButton.gameObject.SetActive(false);
            yield return StartCoroutine(ShowSlide(i));
        }
        SkipIntro();
    }

    IEnumerator ShowSlide(int i)
    {
        centralImage.sprite = slidesSprites[i];
        continueButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);
        var waitForButton = new WaitForUIButtons(continueButton);
        yield return waitForButton.Reset();
    }

    public void SkipIntro()
    {
        GameController.Instance.StartStuff();
        transform.gameObject.SetActive(false);

    }


}
