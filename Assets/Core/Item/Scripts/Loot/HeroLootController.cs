using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
// Credits: danielnyan
[RequireComponent(typeof(IHero))]
public class HeroLootController : MonoBehaviour
{
    private IHero hero;
    private Collider2D looterCollider;

    void Start()
    {
        hero = GetComponent<IHero>();
        looterCollider = hero.GetLooterCollider();
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            var selectedItem = QueryOldestItem();

            if (selectedItem != null)
            {
                selectedItem.OnPickUp(hero);
                SoundManager.Instance.PlaySound(SoundManager.Instance.pickup);
            }
        }
    }

    // Obtains the loot that is about to expire the soonest using the worst possible
    // algoritm: linear search
    private IItem QueryOldestItem()
    {
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        Physics2D.OverlapCollider(looterCollider, filter.NoFilter(), results);
        IItem item = null;
        if (results.Count == 0 || results[0] == false)
        {
            return null;
        }
        float lowest = Mathf.Infinity;
        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject == null ||
                results[i].GetComponent<DropBase>() == null)
            {
                continue;
            }
            DropBase dropBase = results[i].GetComponent<DropBase>();
            float timeRemaining = dropBase.currentDespawnTime;

            if (timeRemaining < lowest)
            {
                lowest = timeRemaining;
                item = dropBase.TakeItem();
            }
        }
        return item;
    }
}
}
