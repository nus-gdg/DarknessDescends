# Character

The character class acts as the exoskeleton for all units. It governs the health of the units and handles them taking damage as well as death events. It provides the following functionality in the unity editor:

* Total Health: The total health of the unit.
* Character Type: Either `Friendly` or `Enemy`. This influences which units can it deal damage to and take damage. There is no friendly fire. If marked as Friendly, then only enemy attacks will cause it to take damage, and if marked as enemy, only friendly attacks will cause it to take damage.

When the character dies, it triggers a CharacterDeathEvent, which is a UnityEvent. It passes in the Character class as a parameter to that event.

# Damager

A damager interacts with the Character to cause the character to lose health. Damage is intended to be given to entities that will come into contact with the Character, such as projectiles and swords. For this to work, both the Character and the Damager must have a 2D collider attached to it. The character should have a rigid body. The damage exposes the following functionality in the unity editor:

* Damage amount: The damage the character will take
* Damage Source: Either `Friendly` or `Enemy`.

Additionally, the damager script can be extended. The _virtual_ method triggerContact is called when the damager comes into contact with a `Character`. By default, it is destroyed. However, it can be extended to instead do nothing (such as a sword swing, which shouldn't disappear) or fork (for special projectile attacks).