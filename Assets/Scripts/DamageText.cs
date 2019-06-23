using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float LifeTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        int SortLayer = 0;
        int SortingLayerID = SortingLayer.NameToID("UI");
        Renderer rend = GetComponentInParent<Renderer>();
        rend.sortingOrder = SortLayer;
        rend.sortingLayerID = SortingLayerID;
        Destroy(gameObject, LifeTime);
    }

    public void SetText(string text, Color color)
    {
        TextMesh tm = GetComponent<TextMesh>();
        tm.text = text;
        tm.color = color;
    }

    void OnDestroy()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
