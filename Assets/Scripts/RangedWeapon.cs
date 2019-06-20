using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RangedWeapon : Weapon
{
    Animator animator;
    bool weaponReady;

    public GameObject projectile;
    public CharacterAnimator characterAnimator;
    public float projectileSpeed;
    public float cooldown;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        weaponReady = true;
    }

    private void Attack()
    {
        animator.SetBool("Attack", true);
        bool facingDirection = characterAnimator.getFacingDirection();

        GameObject spawnedProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        Rigidbody2D rb = spawnedProjectile.GetComponent<Rigidbody2D>();
        Transform trans = spawnedProjectile.GetComponent<Transform>();

        if(facingDirection)
        {
            rb.velocity = new Vector2(projectileSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-projectileSpeed, 0);
            trans.rotation = new Quaternion(trans.rotation.x, 180f, trans.rotation.z, trans.rotation.w);
        }

        weaponReady = false;
        StartCoroutine("WeaponCooldownRoutine");
    }

    public override bool TryToAttack()
    {
        bool retVal = !animator.GetBool("Attack"); //attacking = false, not attacking = true
        if(retVal && weaponReady)
        {
            Attack();
        }
        return retVal;
    }

    public override void StopAttack()
    {
        animator.SetBool("Attack", false);
    }

    IEnumerator WeaponCooldownRoutine()
    {
        yield return new WaitForSeconds(cooldown);
        weaponReady = true;
    }
}