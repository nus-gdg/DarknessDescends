using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
// Credits: sharkcircuits
public class ProjectileWeapon : Weapon
{
    // Can Abstract to Projectile Class
    public GameObject Projectile;
    public float ProjectileSpeed;

    public float Cooldown;

    private bool weaponReady = true;
    private ICharacter owner;

    public override void OnPickUp(ICharacter c)
    {
        base.OnPickUp(c);
        owner = c;
    }

    public override void Use()
    {
        if (!weaponReady) return;
        bool facingDirection = owner.GetFacingDirection();
        SoundManager.Instance.PlayFire();
        GameObject spawnedProjectile = Instantiate(Projectile, transform.position, Quaternion.identity);
        Rigidbody2D rb = spawnedProjectile.GetComponent<Rigidbody2D>();
        Transform trans = spawnedProjectile.GetComponent<Transform>();
        if(facingDirection)
        {
            rb.velocity = new Vector2(ProjectileSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-ProjectileSpeed, 0);
            trans.rotation = new Quaternion(trans.rotation.x, 180f, trans.rotation.z, trans.rotation.w);
        }

        weaponReady = false;
        StartCoroutine("WeaponCooldownRoutine");
    }

    IEnumerator WeaponCooldownRoutine()
    {
        yield return new WaitForSeconds(Cooldown);
        weaponReady = true;
    }
}
}