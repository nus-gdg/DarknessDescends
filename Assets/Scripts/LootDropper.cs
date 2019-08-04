using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
[System.Serializable]
public class LootDropper : MonoBehaviour
{
    //public GameObject Weapon;
    public GameObject LootBase; // Parent gameObject of Weapon so that animation works

    [SerializeField]
    public List<ItemChancePair> possibleLoot = new List<ItemChancePair>();

    [SerializeField]
    public int arraySize; //This needs to be here for the editor to work

    void Start()
    {
        Character character = GetComponent<Character>();
        character.characterDeathEvent.AddListener(DropLoot);
    }

    void DropLoot(Character c, CharacterDeathReason reason)
    {
        if (reason == CharacterDeathReason.KILLED_BY_KILL_ZONE) return; // dont spawn loot if killed by kill zone
        int roll = UnityEngine.Random.Range(1, 101);
        int accum = 0;

        foreach(ItemChancePair icp in possibleLoot)
        {
            accum += icp.percentageChance;
            if(roll <= accum)
            {
                GameObject spawnedLootBase = Instantiate(LootBase, transform.position, Quaternion.identity);
                GameObject spawnedItem = Instantiate(icp.gameObject, Vector3.zero, Quaternion.identity);
                spawnedItem.transform.parent = spawnedLootBase.transform;
                spawnedItem.transform.position = spawnedLootBase.transform.position;
                break;
            }
        }
    }
}

[System.Serializable]
public class ItemChancePair
{
    public GameObject gameObject;
    public int percentageChance;
}
