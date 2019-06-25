using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(BoxCollider2D))]
public class Loot : MonoBehaviour
{
    public float DespawnTime = 5;
    public float currentDespawnTime { get; private set; }

    /*
    [HideInInspector]
    public Weapon WeaponDrop; // assume we have a weapon as a child game object
    void Start()
    {
        WeaponDrop = GetComponentInChildren<Weapon>();
        Assert.IsTrue(WeaponDrop != null);
        StartCoroutine(DespawnInSeconds(DespawnTime));
    }
    */

    bool containsWeapon = false;
    bool containsPowerUp = false;

    [HideInInspector]
    public Item ItemDrop; // assume we have an Item as a child game object

    void Start()
    {
        ItemDrop = GetComponentInChildren<Item>();
        Assert.IsTrue(ItemDrop != null);
        currentDespawnTime = DespawnTime;

        if(ItemDrop is Weapon)
        {
            containsWeapon = true;
        }
        else if(ItemDrop is PowerUp)
        {
            containsPowerUp = true;
        }
    }

    void Update()
    {
        currentDespawnTime -= Time.deltaTime;
        if (currentDespawnTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    }

    public bool isWeapon()
    {
        return containsWeapon;
    }

    public bool isPowerUp()
    {
        return containsPowerUp;
    }
}
