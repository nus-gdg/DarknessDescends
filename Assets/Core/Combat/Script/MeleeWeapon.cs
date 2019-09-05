using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MeleeDamager))]
public class MeleeWeapon : Weapon
{
    private void Attack() {
        this.GetComponent<Animator>().SetBool("Attack", true);
    }

    public override bool TryToAttack() {
        bool rtv = !this.GetComponent<Animator>().GetBool("Attack");
        Attack();
        return rtv;
    }

    public override void StopAttack() {
        this.GetComponent<Animator>().SetBool("Attack", false);
    }
}
