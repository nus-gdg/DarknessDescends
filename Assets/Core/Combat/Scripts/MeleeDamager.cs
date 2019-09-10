using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
// Credits: Wingedevil
public class MeleeDamager : Damager
{
    // Deprecated
    public override void triggerContact()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagee damagee = collision.gameObject.GetComponent<IDamagee>();
        if (damagee != null)
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
        }
    }
}
}
