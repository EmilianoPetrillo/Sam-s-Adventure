using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsVolumeSetup : MonoBehaviour
{
    public GameObject sliderObj;
    void OnEnable()
    {
        sliderObj.GetComponent<MusicVolumeController>().SetSliderValue();
    }
}
