using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLButton : MonoBehaviour
{

    public string url;

    public void OnTap()
    {
        Application.OpenURL(url);
    }
}
