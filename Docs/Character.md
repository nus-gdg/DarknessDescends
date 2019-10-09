# Character

The character class acts as the exoskeleton for all units. It governs the health of the units and handles them taking damage as well as death events. The character class implements the `ICharacter` Interface which provides API to get their stats, pick up items, equip weapons and get various face direction.

It provides the following functionality in the unity editor:

* Character Stats: The base stats of the unit.
* Character Type: Either `Friendly` or `Enemy`. This influences which units can it deal damage to and take damage. There is no friendly fire. If marked as Friendly, then only enemy attacks will cause it to take damage, and if marked as enemy, only friendly attacks will cause it to take damage.
* Health bar atttributes (see below)
* Invulnerability time: Time the character is invulnerable when it is damaged (in seconds). Decimal valaues are allowed.

Character also heandles the UI of the health bar and and reduces it accordingly. For this to work, you need to add a health bar to the prefab, positioning it right above the character. A background color to represent the health (green in this game) should then be added as a child of the health bar. The following then needs to be assigned to the Character script in the Unity editor:

* healthBar: the health bar gameobject
* healthBarGreen: the child of the health bar. It will be adjusted appropriately as the character takes damage. By default, it should have the scaling values of 1 for the x-value.
* healthBarLength: the length of the health bar. This will depend on how wide the sprite is.

Once these values are set, the health bar will start off inactive when the character spawns. Once it takes damage, the health bar appears, showing the reduction in health.

The character also handles the invulnerability session when it is damaged. Set invulnerabilityTime to the amount of time the entity should remain invulnerable if it is damaged. This time is in seconds. You can query if it is currently invulnerable by calling isInvulnerable() method in Character, which returns true if it is invulnerable.

# Hero
In `DarknessDescends` the main character uses the `Hero` script which inherits from `Character`. On death, it will update the appropriate managers to trigger the end of the game.

# Enemy
All other characters would have the `Enemy` script which implements the `IEnemy` interface which specifies behaviour drops and rewards. All enemies the game can drop items and provide a score. This behaviour is implemented in the `Enemy` class. If you wish to write a different kind of `Enemy` class it will have to implement the `IEnemy` interface.

## Drop Behaviours
In the Inspector view, the `Enemy` script will display arraySize. Changing this value will change the number of Item and Drop Rate fields shown.
Here, you can drag the prefab of an Item into the Item field and input its corresponding Drop Rate. The Drop Weightage is similar to a percentage; An item with
a Drop Rate of one will have a 1/(Total Drop Weightage in the Array) of being spawned.
