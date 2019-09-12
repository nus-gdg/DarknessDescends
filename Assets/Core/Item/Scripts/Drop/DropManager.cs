using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
// Credits: sharkcirctuis
public class DropManager : MonoBehaviour
{
    public GameObject DropShell;
    public void DropReward(IDropper dropper)
    {
        Vector3 dropPosition = dropper.GetDropPosition();
        List<DropChancePair> dropList = dropper.GetDropList();

        int roll = UnityEngine.Random.Range(0, dropper.GetDropChanceRange()+1); // not accurate
        int accum = 0;

        foreach (DropChancePair dcp in dropList)
        {
            accum += dcp.weightage;
            if(roll <= accum)
            {
                    dropItem(dropPosition, dcp.dropPrefab);
                    break;
            }
        }
    }
        private void dropItem(Vector3 position, GameObject dropPrefab)
        {
            GameObject dropBase = Instantiate(DropShell, position, Quaternion.identity);
            GameObject droppedItem = Instantiate(dropPrefab, Vector3.zero, Quaternion.identity);
            droppedItem.transform.parent = dropBase.transform;
            droppedItem.transform.position = dropBase.transform.position;

            var item = droppedItem.GetComponent<IItem>();
            var drop = dropBase.GetComponent<DropBase>();
            if (item == null)
            {
                Debug.LogError("Drop Item has no IItem Component");
            }
            else if (drop == null)
            {
                Debug.LogError("Drop Shell has no Drop Base Component");
            }
            else
            {
                drop.itemDrop = item;
            }
        }
}
}
