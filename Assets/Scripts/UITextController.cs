using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextController : MonoBehaviour
{

    public static UITextController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject TapToAttackText;
    public GameObject SwapText;
    public GameObject IntroductionText;
    public Image SwapTextArrow;

    public void DeleteStartStuff()
    {
        Destroy(TapToAttackText.gameObject);
        Destroy(SwapText.gameObject);
        Destroy(IntroductionText.gameObject);
        Destroy(SwapTextArrow.gameObject);
    }

}
