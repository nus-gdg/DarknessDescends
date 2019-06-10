using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChakupiCharacterAnimator : CharacterAnimator
{
    public void BeginCharge()
    {
        animator.SetTrigger("StartCharge");
    }

    public void ContinueCharge(bool isCharging = true)
    {
        animator.SetBool("Charging", isCharging);
    }

    public void Fire()
    {
        animator.SetTrigger("Fire");
    }
}
