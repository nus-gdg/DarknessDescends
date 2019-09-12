using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
[CreateAssetMenu(menuName = "GDG/CharacterStats")]
public class CharacterStats : ScriptableObject, ICharacterStats
{
    [SerializeField]
    private float totalHealth;

    private float health;
    private float speed;

    void OnEnable()
    {
        health = totalHealth;
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    public float GetTotalHealth()
    {
        return totalHealth;
    }

    public float GetCurrentSpeed()
    {
        return speed;
    }

    public void Damage(float amount)
    {
        health -= amount;
        health = health < 0 ? 0 : health;
    }

    public void Heal(float amount)
    {
        health += amount;
        health = health > totalHealth ? totalHealth : health;
    }

    public void UpdateSpeed(float amount)
    {
        speed = amount;
    }
}
}
