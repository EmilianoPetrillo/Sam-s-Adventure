using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HideLoadPanel();
    }

    public GameObject LoadPanel;

    public TextMeshProUGUI LoadingText;
    public TextMeshProUGUI LoadPercentText;

    public Image LoadingBar;

    public void ShowLoadText(string _text)
    {
        LoadPanel.SetActive(true);
        LoadingText.text = _text;
        LoadingBar.fillAmount = 0;
    }
    public void UpdateLoadPercent(float percent)
    {
        LoadingBar.fillAmount = percent;
    }
    public void UpdateLoadPercent(int percent)
    {
        UpdateLoadPercent((float)percent / 100f);
    }

    public void HideLoadPanel()
    {
        LoadPanel.SetActive(false);
    }
}
