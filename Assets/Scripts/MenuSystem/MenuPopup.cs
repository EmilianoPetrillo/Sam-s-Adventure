using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MenuPopup : MonoBehaviour
{
    public GameObject PopupPanel;

    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI MessageText;

    public TextMeshProUGUI ButtonText1;
    public TextMeshProUGUI ButtonText2;
    public TextMeshProUGUI ButtonText3;
    public Button Button1;
    public Button Button2;
    public Button Button3;

    private void Start()
    {
        PopupPanel.SetActive(false);
    }

    public void ShowPopup(string title, string message, string buttonText1, UnityEvent callback1)
    {
        TitleText.text = title;
        MessageText.text = message;

        ButtonText1.text = buttonText1;
        Button1.onClick.RemoveAllListeners();
        Button1.onClick.AddListener(delegate { callback1?.Invoke(); HidePopup(); });

        PopupPanel.SetActive(true);
    }

    public void ShowPopup(string title, string message, string buttonText1, string buttonText2, UnityEvent callback1, UnityEvent callback2)
    {
        TitleText.text = title;
        MessageText.text = message;

        ButtonText1.text = buttonText1;
        ButtonText2.text = buttonText2;
        Button1.onClick.RemoveAllListeners();
        Button1.onClick.AddListener(delegate { callback1?.Invoke(); HidePopup(); });
        Button2.onClick.RemoveAllListeners();
        Button2.onClick.AddListener(delegate { callback2?.Invoke(); HidePopup(); });

        PopupPanel.SetActive(true);
    }

    public void ShowPopup(string title, string message, string[] buttonText, UnityEvent[] callbacks)
    {
        ShowPopup(title, message, buttonText[0], buttonText[1], buttonText[2], callbacks[0], callbacks[1], callbacks[2]);
    }

    public void ShowPopup(string title, string message, string buttonText1, string buttonText2, string buttonText3, UnityEvent callback1, UnityEvent callback2, UnityEvent callback3)
    {
        TitleText.text = title;
        MessageText.text = message;

        ButtonText1.text = buttonText1;
        ButtonText2.text = buttonText2;
        ButtonText3.text = buttonText3;
        Button1.onClick.RemoveAllListeners();
        Button1.onClick.AddListener(delegate { callback1?.Invoke(); HidePopup(); });
        Button2.onClick.RemoveAllListeners();
        Button2.onClick.AddListener(delegate { callback2?.Invoke(); HidePopup(); });
        Button3.onClick.RemoveAllListeners();
        Button3.onClick.AddListener(delegate { callback3?.Invoke(); HidePopup(); });

        PopupPanel.SetActive(true);
    }

    public void HidePopup()
    {
        PopupPanel.SetActive(false);
    }
}
