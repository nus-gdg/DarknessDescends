using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WeaponController : MonoBehaviour
{
    public Transform MeleeWeaponTransform;

    //Changes
    public Transform RangedWeaponTransform;
    //Changes

    public Weapon WieldedWeapon;
    private HashSet<GameObject> overlappingItems = new HashSet<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == null) return;
        if (other.GetComponent<Loot>() != null && !overlappingItems.Contains(other.gameObject))
        {
            overlappingItems.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == null) return;
        if (overlappingItems.Contains(other.gameObject))
        {
            overlappingItems.Remove(other.gameObject);
        }
    }

    // Obtains the loot that is about to expire the soonest using the worst possible
    // algoritm: linear search
    private GameObject QueryOldestItem()
    {
        if (overlappingItems.Count == 0)
        {
            return null;
        }
        GameObject[] gameObjects = new GameObject[overlappingItems.Count];
        overlappingItems.CopyTo(gameObjects);
        float lowest = Mathf.Infinity;
        int index = -1;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i] == null)
            {
                overlappingItems.Remove(gameObjects[i]);
                continue;
            }
            float timeRemaining = gameObjects[i].GetComponent<Loot>().currentDespawnTime;
            if (timeRemaining < lowest)
            {
                lowest = timeRemaining;
                index = i;
            }
        }
        if (index == -1) return null;
        return gameObjects[index];
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
            if (drop is MeleeWeapon)
            {
                drop.transform.parent = MeleeWeaponTransform;
                drop.transform.position = MeleeWeaponTransform.position;
                Vector3 weaponScale = drop.transform.localScale;
                drop.transform.localScale =
                    new Vector3(weaponScale.x * Mathf.Sign(transform.localScale.x),
                                weaponScale.y,
                                weaponScale.z);
            }

            //Changes
            if(drop is RangedWeapon)
            {
                drop.transform.parent = RangedWeaponTransform;
                drop.transform.position = MeleeWeaponTransform.position;
                Vector3 weaponScale = drop.transform.localScale;
                drop.transform.localScale =
                    new Vector3(weaponScale.x * Mathf.Sign(transform.localScale.x),
                                weaponScale.y,
                                weaponScale.z);

                ((RangedWeapon)drop).characterAnimator = GetComponent<CharacterAnimator>();
            }
            //Changes

            loot.ItemDrop = null;
            WieldedWeapon = drop;
            Destroy(loot.gameObject);

            overlappingItems.Remove(loot.gameObject);
            SoundController.theController.playSound(SoundController.theController.pickup);
        }
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
