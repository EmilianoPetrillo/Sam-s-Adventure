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

    private float t;
    private bool timer = true;

    private void Update()
    {
        if(timer)
            t += Time.deltaTime;
        if(t > 5)
        {
            t = 0;
            timer = false;
            Destroy(TapToAttackText);
        }
    }
}
