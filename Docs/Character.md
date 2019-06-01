# Character

The character class acts as the exoskeleton for all units. It governs the health of the units and handles them taking damage as well as death events. It provides the following functionality in the unity editor:

* Total Health: The total health of the unit.
* Character Type: Either `Friendly` or `Enemy`. This influences which units can it deal damage to and take damage. There is no friendly fire. If marked as Friendly, then only enemy attacks will cause it to take damage, and if marked as enemy, only friendly attacks will cause it to take damage.
* Health bar atttributes (see below)
* Invulnerability time: Time the character is invulnerable when it is damaged (in seconds). Decimal valaues are allowed.

When the character dies, it triggers a CharacterDeathEvent, which is a UnityEvent. It passes in the Character class as a parameter to that event.

Character also heandles the UI of the health bar and and reduces it accordingly. For this to work, you need to add a health bar to the prefab, positioning it right above the character. A background color to represent the health (green in this game) should then be added as a child of the health bar. The following then needs to be assigned to the Character script in the Unity editor:

* healthBar: the health bar gameobject
* healthBarGreen: the child of the health bar. It will be adjusted appropriately as the character takes damage. By default, it should have the scaling values of 1 for the x-value.
* healthBarLength: the length of the health bar. This will depend on how wide the sprite is.

Once these values are set, the health bar will start off inactive when the character spawns. Once it takes damage, the health bar appears, showing the reduction in health.

The character also handles the invulnerability session when it is damaged. Set invulnerabilityTime to the amount of time the entity should remain invulnerable if it is damaged. This time is in seconds. You can query if it is currently invulnerable by calling isInvulnerable() method in Character, which returns true if it is invulnerable.

# Damager

A damager interacts with the Character to cause the character to lose health. Damage is intended to be given to entities that will come into contact with the Character, such as projectiles and swords. For this to work, both the Character and the Damager must have a 2D collider attached to it. The character should have a rigid body. The damage exposes the following functionality in the unity editor:

* Damage amount: The damage the character will take
* Damage Source: Either `Friendly` or `Enemy`.
* Force: push-back for the opponent when it takes damage.

Additionally, the damager script can be extended. The _virtual_ method triggerContact is called when the damager comes into contact with a `Character`. By default, it is destroyed. However, it can be extended to instead do nothing (such as a sword swing, which shouldn't disappear) or fork (for special projectile attacks).