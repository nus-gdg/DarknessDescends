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

        private List<GameObject> lootInRange;
        void Start()
        {
            hero = GetComponent<IHero>();
            lootInRange = new List<GameObject>();
            var boxCollider = gameObject.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {

                if (lootInRange.Count > 0)
                {
                    GameObject go = lootInRange[0];
                    lootInRange.RemoveAt(0);

                    DropBase drop = go.GetComponent<DropBase>();
                    IItem randomitem = drop.TakeItem();
                    randomitem.OnPickUp(hero);
                    SoundManager.Instance.PlaySound(SoundManager.Instance.pickup);

                    Debug.Log("Item Looted");
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Item")
            {
                lootInRange.Add(other.gameObject);
                Debug.Log("Item in Range");
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Item")
            {
                lootInRange.Remove(other.gameObject);
                Debug.Log("Item out of Range");
            }
        }

        /*
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
    }*/
    }
}
