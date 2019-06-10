using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Character>().KillAndDestroy();
        }
        else
        {
            RecursiveDestroy(other.gameObject);
        }
    }

    void RecursiveDestroy(GameObject gameObject)
    {
        if(gameObject.transform.parent == null)
        {
            Destroy(gameObject);
        }
        else
        {
            RecursiveDestroy(gameObject.transform.parent.gameObject);
        }
    }
}
