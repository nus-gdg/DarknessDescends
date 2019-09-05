using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponController : MonoBehaviour
{
    public Transform HandTransform;
    public Weapon WieldedWeapon;

    private Collider2D playerCollider;

    // Obtains the loot that is about to expire the soonest using the worst possible
    // algoritm: linear search
    private GameObject QueryOldestItem()
    {
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        Physics2D.OverlapCollider(playerCollider, filter.NoFilter(),  results);
        if (results[0] == false)
        {
            return null;
        }
        float lowest = Mathf.Infinity;
        int index = -1;
        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject == null || results[i].GetComponent<Loot>() == null)
            {
                continue;
            }
            float timeRemaining = results[i].GetComponent<Loot>().currentDespawnTime;
            if (timeRemaining < lowest)
            {
                lowest = timeRemaining;
                index = i;
            }
        }
        if (index == -1) return null;
        return results[index].gameObject;
    }

    private void Pickup(GameObject item)
    {
        Loot loot = item.GetComponent<Loot>();
        if (loot != null && loot.isWeapon())
        {
            if (WieldedWeapon != null)
            {
                Destroy(WieldedWeapon.gameObject);
            }
            WieldedWeapon = null;
            Weapon drop = (Weapon)loot.ItemDrop;
            drop.AttachToTransform(HandTransform, transform.localScale.x >= 0);
            loot.ItemDrop = null;
            WieldedWeapon = drop;
            Destroy(loot.gameObject);
            SoundController.theController.playSound(SoundController.theController.pickup);
        }
    }

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject selectedItem = QueryOldestItem();
            if (selectedItem)
            {
                Pickup(selectedItem);
            }
        }
        if (WieldedWeapon == null) return;
        if (Input.GetKey(KeyCode.E)) {
            WieldedWeapon.TryToAttack();
        } else {
            WieldedWeapon.StopAttack();
        }
    }
}
