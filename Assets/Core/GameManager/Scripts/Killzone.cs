using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDG;

public class Killzone : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        if((other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") &&
            other.gameObject.GetComponent<Character>()) // to refactor (currently enemy weapons are tagged as "enemy")
        {
            other.gameObject.GetComponent<Character>().KillAndDestroy(CharacterDeathReason.KILLED_BY_KILL_ZONE);
        }
        else if (other.gameObject.GetComponent<Damager>())
        {
            other.gameObject.GetComponent<Damager>().triggerContact(); // despawns damagers if they are projectiles
        }
        else
        {
            Destroy(gameObject.transform.root.gameObject);
        }
    }
}
