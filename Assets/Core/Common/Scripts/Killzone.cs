using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
public class Killzone : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        if((other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") &&
            other.gameObject.GetComponent<Character>()) // to refactor (currently enemy weapons are tagged as "enemy")
        {
            other.gameObject.GetComponent<Character>().KillAndDestroy(CharacterDeathReason.KILLED_BY_KILL_ZONE);
        }
        else
        {
            Destroy(other.gameObject.transform.root.gameObject);
        }
    }
}
}
