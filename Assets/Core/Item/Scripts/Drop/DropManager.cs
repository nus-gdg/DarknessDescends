using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
// Credits: sharkcirctuis
public class DropManager : MonoBehaviour
{
    public void DropReward(IDropper dropper)
    {
        Vector3 dropPosition = dropper.GetDropPosition();
        List<DropChancePair> dropList = dropper.GetDropList();

        int roll = UnityEngine.Random.Range(1, 101); // not accurate
        int accum = 0;

        foreach (DropChancePair dcp in dropList)
        {
            accum += dcp.percentageChance;
            if(roll <= accum)
            {
                GameObject droppedItem = Instantiate(dcp.dropPrefab, dropPosition, Quaternion.identity);
                break;
            }
        }
    }
}
}
