using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpit : MonoBehaviour
{
    OldGolem oldGolem;
    Animator animator;

    //private float t = 0;
    
    private void Start()
    {
        oldGolem = GetComponentInParent<OldGolem>();
        animator = GetComponent<Animator>();
    }

    //private void Update()
    //{
    //    t += Time.deltaTime;
    //    if (t >= animator.GetCurrentAnimatorStateInfo(0).length)
    //    {
    //        t = 0;
    //        mechaStone.EndLaserBeam();
    //    }
    //}

    public void DoDamage()
    {
        oldGolem.DoDamage();
    }
}
