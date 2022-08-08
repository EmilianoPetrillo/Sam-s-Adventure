using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageSubmenusPanel : MonoBehaviour
{
    public GameObject[] StuffToClose;
    public GameObject[] StuffToOpen;
    public GameObject otherPanel;
    public void TogglePanel()
    {
        bool isActive = StuffToOpen[0].activeSelf;
        if(otherPanel.activeSelf)
        {
            otherPanel.SetActive(false);
        }
        for (int i = 0; i < StuffToClose.Length; i++)
            StuffToClose[i].gameObject.SetActive(isActive);
        for (int i = 0; i < StuffToOpen.Length; i++)
            StuffToOpen[i].gameObject.SetActive(!isActive);
    }
}
