using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
[RequireComponent(typeof(Hero))]
public class HeroWeaponController : MonoBehaviour
{
    Hero hero;
    void Start()
    {
        hero = GetComponent<Hero>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var weapon = hero.GetWeapon();
            if (weapon != null)
            {
                weapon.Use();
            }
        }
    }
}
}
