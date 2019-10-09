using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{

// Applies Damage Once
public class OneHitDamager : Damager
{
    protected override void ApplyDamage(IDamagee damagee, Collider2D collision)
    {
        if (damagee.GetAllyType() == damageSource || // friendly
            !damagee.IsVulnerableToDamage())         // immune to damage
        {
            return;
        }
        float deltaX = collision.transform.position.x - transform.position.x;
        Vector3 impulse = forceDirection * force;
        impulse.x *= (deltaX >= 0 ? 1 : -1);
        damagee.Damage(damageAmount);
        damagee.KnockBackFromDamage(impulse);
        Destroy(gameObject.transform.root.gameObject);
    }
}
}
