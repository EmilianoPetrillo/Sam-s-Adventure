using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel( float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSliderValue()
    {
        float mixerValue;
        mixer.GetFloat("MusicVol", out mixerValue);
        mixerValue = mixerValue / 20;
        mixerValue = Mathf.Pow(10, mixerValue);
        transform.gameObject.GetComponent<Slider>().value = mixerValue;
    }
}
