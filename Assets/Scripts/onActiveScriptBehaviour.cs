using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onActiveScriptBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetChild(0).gameObject.GetComponent<CutscenesTypedText>().StartShowSlides();
    }
}
