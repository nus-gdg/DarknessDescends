using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class LootDropper : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject LootBase; // Parent gameObject of Weapon so that animation works
    void Start()
    {
        Character character = GetComponent<Character>();
        character.characterDeathEvent.AddListener(DropLoot);
    }

    void DropLoot(Character c)
    {
        // Loot
        GameObject spawnedLoot = Instantiate(LootBase, transform.position, Quaternion.identity);
        GameObject spawnedWeapon = Instantiate(Weapon, Vector3.zero, Quaternion.identity);
        spawnedWeapon.transform.parent = spawnedLoot.transform;
    }
}
