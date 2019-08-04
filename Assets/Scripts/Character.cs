using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public int totalHealth;
	private int health;

	public CharacterDeathEvent characterDeathEvent;
    public CharacterInjuredEvent characterInjuredEvent;
	public AllyType characterType;

    public int rewardUponDeath;

    public GameObject healthBar;
    public GameObject healthBarGreen;
    public float healthBarLength;


    public float invulnerabilityTime;
    private float invulnCounter;

    private GameManager gameManager;
    protected Animator animator;
    private MovementController controller;

    void Awake() {
        healthBar.SetActive(false);
        health = totalHealth;
        animator = GetComponent<Animator>();
        characterDeathEvent = new CharacterDeathEvent();
        characterInjuredEvent = new CharacterInjuredEvent();
        controller = GetComponent<MovementController>();
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        characterInjuredEvent.AddListener(DamageTextManager.Instance.OnCharacterInjured);
    }

    public void Update() {
        if (invulnCounter > 0) {
            invulnCounter -= Time.deltaTime;
        } else if (invulnCounter < 0) {
            SetVulnerable(); // remove invulnerability
        }
    }

    void applyKnockBack(Vector3 impulse) {
        controller.KnockBack(impulse);
        controller.MovementEnabled = false;
    }

    void takeDamage(int i)
    {
        if (animator.GetBool("Crouch") != true)
        {
            health -= i;
            characterInjuredEvent.Invoke(this, i);

            if (i > 0)
            {
                SoundController.theController.playSound(SoundController.theController.damage);
            }

            if (health <= 0)
            {
                this.KillAndDestroy();
            }
            else if (health > totalHealth)
            {
                health = totalHealth;
            }

            healthBar.SetActive(true);
            Vector3 temp = healthBarGreen.transform.localScale;
            temp.x = (((float)health) / totalHealth);
            healthBarGreen.transform.localScale = temp;

            temp = healthBarGreen.transform.localPosition;
            temp.x = healthBarLength * (1 - (((float)health) / totalHealth)) * -0.5f;
            healthBarGreen.transform.localPosition = temp;
        }
        else
        {
            CrouchScript crouch = GetComponent<CrouchScript>();
            crouch.ShieldVal -= i;
        }
    }

    public void Heal(int i)
    {
        health += i;

        if(health > totalHealth)
        {
            health = totalHealth;
        }

        Vector3 temp = healthBarGreen.transform.localScale;
        temp.x = (((float) health)/ totalHealth);
        healthBarGreen.transform.localScale = temp;

        temp = healthBarGreen.transform.localPosition;
        temp.x = healthBarLength * (1 - (((float) health)/totalHealth)) * 0.5f;
        healthBarGreen.transform.localPosition = temp;
    }

    public void KillAndDestroy()
    {
        characterDeathEvent.Invoke(this);
        KillCharacter();
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        Damager damageComponent = collision.gameObject.GetComponent<Damager>();
        if (damageComponent != null) {
            onCollision(damageComponent);
        }

        PowerUp powerUp = collision.gameObject.GetComponent<PowerUp>();
        if(powerUp != null)
        {
            powerUp.InteractWithPowerUp(this);
            SoundController.theController.playSound(SoundController.theController.pickup);
        }
    }

    public void SetVulnerable() {
        invulnCounter = 0.0f;
        controller.MovementEnabled = true;
        controller.Stationary(); // reset x velocity after invulnerability ends
    }

    private void onCollision(Damager damageComponent) {
        if (damageComponent.damageSource != characterType && !isInvulnerable()) {
            float deltaX = this.transform.position.x - damageComponent.transform.position.x;
            Vector3 impulse = damageComponent.forceDirection * damageComponent.force;
            impulse.x *= (deltaX >= 0 ? 1 : -1);
            this.applyKnockBack(impulse);
            this.takeDamage(damageComponent.damageAmount);
            if (animator.GetBool("Crouch") != true)
            {
                this.GetComponent<CharacterAnimator>().Hurt();
            }
            damageComponent.triggerContact();
            invulnCounter = invulnerabilityTime;
        }
    }

    public bool isInvulnerable() {
        return invulnCounter > 0;
    }

    void KillCharacter()
    {
        if(gameObject.tag == "Player")
        {
            gameManager.playerCharacterDies();
        }
        else
        {
            gameManager.enemyDies(rewardUponDeath);
        }
    }
}