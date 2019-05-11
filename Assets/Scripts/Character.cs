using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public int totalHealth;
	private int health;
	public CharacterDeathEvent characterDeathEvent;

    void Awake()
    {
        health = totalHealth;
        if (characterDeathEvent == null) {
        	characterDeathEvent = new CharacterDeathEvent();
        }
    }

    void takeDamage(int i) {
    	health -= totalHealth;
    	if(health <= 0) {
    		characterDeathEvent.Invoke(this);
    		Destroy(gameObject);
    	}
    }

    void OnCollisionEnter(Collision collision) {
    	//if(collision.gameObject ... ) 
    }
}
