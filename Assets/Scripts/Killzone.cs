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
        else if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject.transform.root.gameObject);
        }
    }
}
