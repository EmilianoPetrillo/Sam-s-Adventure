using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    MechaStone mechaStone;
    Animator animator;

    private float t = 0;

    private void Start()
    {
        mechaStone = GetComponentInParent<MechaStone>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        t += Time.deltaTime;
        if(t >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            t = 0;
            mechaStone.EndLaserBeam();
        }
    }

    public void DoDamage()
    {
        mechaStone.DoDamage();
    }
}
