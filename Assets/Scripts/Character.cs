using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public int totalHealth;
	private int health;

	public CharacterDeathEvent characterDeathEvent;
	public AllyType characterType;

    public GameObject healthBar;
    public GameObject healthBarGreen;
    public float healthBarLength;

    public float invulnerabilityTime;
    private float invulnCounter;

	void Awake()
	{
		setup();
	}

    public void setup() {
        healthBar.SetActive(false);
        health = totalHealth;
        if (characterDeathEvent == null) {
            characterDeathEvent = new CharacterDeathEvent();
        }
    }

    public void Update() {
        if (invulnCounter > 0) {
            invulnCounter -= Time.deltaTime;
        }
    }

	void takeDamage(int i) {
		health -= i;

        if(i > 0) {
            SoundController.theController.playSound(SoundController.theController.damage);
        }

		if (health <= 0) {
			characterDeathEvent.Invoke(this);
			Destroy(gameObject);
		} else if(health > totalHealth) {
            health = totalHealth;
        }

        healthBar.SetActive(true);
        Vector3 temp = healthBarGreen.transform.localScale;
        temp.x = (((float) health)/totalHealth);
        healthBarGreen.transform.localScale = temp;

        temp = healthBarGreen.transform.localPosition;
        temp.x = healthBarLength * (1 - (((float) health)/totalHealth)) * -0.5f;
        healthBarGreen.transform.localPosition = temp;
	}

    void OnCollisionEnter2D(Collision2D collision) {
        Damager damageComponent = collision.gameObject.GetComponent<Damager>();
        if (damageComponent != null) {
            onCollision(damageComponent);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Damager damageComponent = collision.gameObject.GetComponent<Damager>();
        if (damageComponent != null) {
            onCollision(damageComponent);
        }
    }

    private void onCollision(Damager damageComponent) {
        if (damageComponent.damageSource != characterType && !isInvulnerable()) {
            this.takeDamage(damageComponent.damageAmount);
            this.GetComponent<CharacterAnimator>().Hurt();
            damageComponent.triggerContact();
            invulnCounter = invulnerabilityTime;
        }
    }

    public bool isInvulnerable() {
        return invulnCounter > 0;
    }
}
