using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
	public int damageAmount;
	public AllyType damageSource;

	public float force;

	public virtual void triggerContact() {
		Destroy(gameObject);
	}
}
