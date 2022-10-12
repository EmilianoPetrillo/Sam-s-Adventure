using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public static Fader Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject faderObject;

    bool FadingIn;
    bool FadingOut;

    float duration;
    float startTime;

    UnityEvent OnFadeIn = new UnityEvent();
    UnityEvent OnFadeOut = new UnityEvent();

    public Image FadeImage;
    Color fadeColor = Color.black;

    public const float GLOBAL_FADE_DURATION = 2f;

    public void SetFade(float _alpha)
    {
        fadeColor.a = _alpha;
        FadeImage.color = fadeColor;
    }
    public void FadeIn(float _duration = GLOBAL_FADE_DURATION, UnityAction onComplete = null)
    {
        FadeImage.gameObject.SetActive(true);

        FadingIn = true;
        FadingOut = false;

        duration = _duration;
        startTime = Time.unscaledTime;

        OnFadeIn.RemoveAllListeners();
        OnFadeIn.AddListener(() => onComplete?.Invoke());
    }

    public void FadeOut(float _duration = GLOBAL_FADE_DURATION, UnityAction onComplete = null)
    {
        FadeImage.gameObject.SetActive(true);

        FadingOut = true;
        FadingIn = false;

        duration = _duration;
        startTime = Time.unscaledTime;

        OnFadeOut.RemoveAllListeners();
        OnFadeOut.AddListener(() => onComplete?.Invoke());
    }

    private void Update()
    {
        float t = Mathf.Clamp01((Time.unscaledTime - startTime) / duration);
        if (FadingIn)
        {
            fadeColor.a = Mathf.Clamp01(t);
            FadeImage.color = fadeColor;
         
            if (t >= 1f)
            {
                FadingIn = false;
                OnFadeIn?.Invoke();
            }
        }
        else if (FadingOut)
        {
            t = 1 - t;

            fadeColor.a = Mathf.Clamp01(t);
            FadeImage.color = fadeColor;

            if (t <= 0)
            {
                FadingOut = false;
                OnFadeOut?.Invoke();
                FadeImage.gameObject.SetActive(false);
            }
        }
    }
}
