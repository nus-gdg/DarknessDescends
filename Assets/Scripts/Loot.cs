using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


[RequireComponent(typeof(BoxCollider2D))]
public class Loot : MonoBehaviour
{
    [HideInInspector]
    public Weapon WeaponDrop; // assume we have a weapon as a child game object
    void Start()
    {
        WeaponDrop = GetComponentInChildren<Weapon>();
        Assert.IsTrue(WeaponDrop != null);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    }
}
