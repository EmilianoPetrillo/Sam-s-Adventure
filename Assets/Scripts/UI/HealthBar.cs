using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Text hpText;
    public Image fillColor;
    public bool enableGradient;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        hpText.text = health.ToString() + "HP";
        if(enableGradient)
            fillColor.color = gradient.Evaluate(1f);
    }
    public void SetHealth(float health)
    {
        slider.value = health;
        hpText.text = health.ToString() + "HP";
        if (enableGradient)
            fillColor.color = gradient.Evaluate(slider.normalizedValue);
    }
}
