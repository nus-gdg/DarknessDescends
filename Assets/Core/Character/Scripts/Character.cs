using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
public class Character : MonoBehaviour, ICharacter
{

    [SerializeField]
    private AllyType characterType;
    [SerializeField]
    private CharacterStats characterStats;
    [SerializeField]
    private CharacterInjuredEvent characterInjuredEvent;
    [SerializeField]
    private Transform hand;

    public GameObject healthBar;
    public GameObject healthBarGreen;
    public float healthBarLength;

    [SerializeField]
    private float invulnerabilityTime;
    private float invulnCounter;

    private IItem weapon;
    private MovementController controller;
    private CharacterAnimator characterAnimator;

    public AllyType CharacterType { get => characterType; }
    public CharacterStats CharacterStats { get => characterStats; }
    public CharacterInjuredEvent CharacterInjuredEvent { get => characterInjuredEvent; }

    void Awake()
    {
        healthBar.SetActive(false);
        controller = GetComponent<MovementController>();
        characterAnimator = GetComponent<CharacterAnimator>();
        characterStats = Instantiate(characterStats);
    }

    void Update()
    {
        if (invulnCounter > 0)
        {
            invulnCounter -= Time.deltaTime;
        }
        else if (invulnCounter < 0)
        {
            SetVulnerable(); // remove invulnerability
        }
    }

    public ICharacterStats GetCharacterStats()
    {
        return characterStats;
    }

    public void PickUp(IItem item)
    {
        SoundManager.Instance.PlayPickup();
        item.OnPickUp(this);
    }

    public IItem GetWeapon()
    {
        return weapon;
    }

    public void EquipAsWeapon(IItem weapon)
    {
        if (this.weapon != null)
        {
            this.weapon.Destroy();
        }
        this.weapon = weapon;
        weapon.SetParentTransform(hand);
    }

    public AllyType GetAllyType()
    {
        return characterType;
    }

    public bool IsVulnerableToDamage()
    {
        return !isInvulnerable();
    }

    public void KnockBackFromDamage(Vector3 impulse)
    {
        this.applyKnockBack(impulse);
    }

    public void Damage(float amount)
    {
        if (characterStats.GetCurrentHealth() <= 0 ||
            amount > 0 ||
            IsVulnerableToDamage())
        {
            invulnCounter = invulnerabilityTime;
            characterStats.Damage(amount);
            SoundManager.Instance.PlayDamage();
            characterAnimator.Hurt();

            var currentHealth = characterStats.GetCurrentHealth();
            var totalHealth = characterStats.GetTotalHealth();

            EventManager.Instance.Raise(new CharacterDamagedEvent {
                Damage = amount,
                Position = transform.position,
                CharacterType = GetAllyType(),
            });

            if (currentHealth <= 0)
            {
                this.KillAndDestroy(CharacterDeathReason.KILLED_BY_DAMAGE);
                return;
            }

            healthBar.SetActive(true);
            Vector3 temp = healthBarGreen.transform.localScale;
            temp.x = currentHealth / totalHealth;
            healthBarGreen.transform.localScale = temp;

            temp = healthBarGreen.transform.localPosition;
            temp.x = healthBarLength * (1 - ((currentHealth) / totalHealth)) * -0.5f;
            healthBarGreen.transform.localPosition = temp;
        }
    }

    public void Heal(float i)
    {
        characterStats.Heal(i);
        var currentHealth = characterStats.GetCurrentHealth();
        var totalHealth = characterStats.GetTotalHealth();
        if (currentHealth > totalHealth)
        {
            currentHealth = totalHealth;
        }

        Vector3 temp = healthBarGreen.transform.localScale;
        temp.x = currentHealth / totalHealth;
        healthBarGreen.transform.localScale = temp;

        temp = healthBarGreen.transform.localPosition;
        temp.x = healthBarLength * (1 - ((currentHealth) / totalHealth)) * 0.5f;
        healthBarGreen.transform.localPosition = temp;
    }

    public virtual void KillAndDestroy(CharacterDeathReason reason)
    {
        Destroy(gameObject);
    }

    public void SetVulnerable()
    {
        invulnCounter = 0.0f;
        controller.MovementEnabled = true;
        controller.Stationary(); // reset x velocity after invulnerability ends
    }

    void applyKnockBack(Vector3 impulse)
    {
        controller.KnockBack(impulse);
        controller.MovementEnabled = false;
    }

    public bool isInvulnerable()
    {
        return invulnCounter > 0;
    }

    public bool GetFacingDirection()
    {
        return characterAnimator.getFacingDirection();
    }
}

public class CharacterDamagedEvent : GameEvent
{
    public float Damage;
    public Vector3 Position;
    public AllyType CharacterType;
}
}