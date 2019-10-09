using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
public interface IDamagee
{
    AllyType GetAllyType();
    bool IsVulnerableToDamage();
    void Damage(float amount);
    void KnockBackFromDamage(Vector3 impulse);
}

public interface IHealee
{
    void Heal(float amount);
}
}