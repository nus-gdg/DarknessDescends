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

    public bool TryToAttack() {
        bool rtv = !this.GetComponent<Animator>().GetBool("Attack");
        Attack();
        return rtv;
    }

    public void StopAttack() {
        this.GetComponent<Animator>().SetBool("Attack", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
