using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LootDropper))]
public class LootDropperEditor : Editor
{   
    public override void OnInspectorGUI()
    {
        LootDropper myLootDropper = (LootDropper)target;

        //Handling the LootBase
        myLootDropper.LootBase = (GameObject)EditorGUILayout.ObjectField("Loot Base", myLootDropper.LootBase, typeof(GameObject), true);

        //Handling the lootList
        List<ItemChancePair> lootList = myLootDropper.possibleLoot;

        myLootDropper.arraySize = EditorGUILayout.IntField("Size", myLootDropper.arraySize);
        myLootDropper.arraySize = myLootDropper.arraySize < 0 ? 0 : myLootDropper.arraySize;

        int originalCapacity = lootList.Capacity;

        if(myLootDropper.arraySize > originalCapacity)
        {
            lootList.Capacity = myLootDropper.arraySize;
        }

        for(int i = originalCapacity; i < myLootDropper.arraySize; i++)
        {
            lootList.Add(new ItemChancePair());
        }

        for(int i = 0; i < myLootDropper.arraySize; i++)
        {
            ItemChancePair icp = lootList[i];

            GUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 40;
            icp.gameObject = (GameObject)EditorGUILayout.ObjectField("Item", icp.gameObject, typeof(GameObject), true);
            EditorGUIUtility.labelWidth = 70;
            icp.percentageChance = EditorGUILayout.IntField("Drop Rate", icp.percentageChance);
            GUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(myLootDropper);
        }
    }
}