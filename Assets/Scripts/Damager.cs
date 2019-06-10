using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {
    public float force;
    public Vector3 forceDirection;
    public int damageAmount;
	public AllyType damageSource;

    private void Start() {
        forceDirection = Vector3.Normalize(forceDirection);
    }

    public virtual void triggerContact() {
		//Destroy(gameObject);
        RecursiveDestroy(gameObject);
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
