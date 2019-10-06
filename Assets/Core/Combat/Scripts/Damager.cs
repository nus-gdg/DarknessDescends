using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
//Credits: Wingedevil
[RequireComponent(typeof(Collider2D))]
public class Damager : MonoBehaviour
{
    public float force;
    public Vector3 forceDirection;
    public int damageAmount;
    public AllyType damageSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagee damagee = collision.gameObject.GetComponent<IDamagee>();
        if (damagee != null)
        {
            ApplyDamage(damagee, collision);
        }
    }

    protected virtual void ApplyDamage(IDamagee damagee, Collider2D collision)
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
