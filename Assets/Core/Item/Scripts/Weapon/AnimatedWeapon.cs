using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
// Credits: Wingedevil
[RequireComponent(typeof(Animator))]
public class AnimatedWeapon : Weapon
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void Use()
    {
        animator.SetTrigger("Attack");
    }
}
}