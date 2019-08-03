# Item
The Item class is a catch-all abstract class that represents everything that can be dropped as loot. This includes all weapons and power-ups.

# Weapon
The Weapon is an abstract class. The player must explicitly pick them up in order to use them. The Weapon has two methods to implement, TryToAttack 
and StopAttack. The TryToAttack method should return true if the attack was successful and false otherwise. The StopAttack method should end any 
ongoing attacks.

# WeaponController
The OnTriggerEnter2D and OnTriggerExit2D methods take note of the Weapons that the Player is currently able to pick up. When the Player presses the 
key mapped to Weapon Pickups, the Pickup method is called. This will destroy any Weapon the Player is currently holding and attach the new Weapon to 
the Transform.

The QueryOldestWeapon method exists to establish a logic for when the Player is able to pick up more than one Weapon. It returns a reference to the 
weapon that will expire the soonest.

# PowerUp
The PowerUp is an abstract class. Unlike Weapons, their effect should be triggered upon contact with the Player Character and should not require the 
player to actively pick them up. Upon contact with the player, the method “InteractWithPowerUp” should be called.

# Healthpack
The Healthpack represents a basic implementation of a PowerUp. Its implementation of “InteractWithPowerUp” does two things: it heals the Player by 
an amount set by the developer, and then destroys itself.

# LootDropper
The LootDropper is a Script that should be attached to GameObjects (typically Enemies) that you intend to be able to drop items.

The LootDropper has 3 public variables.
* LootBase: A reference ot the prefab with the same name, which exists to hold the dropped loot.
* possibleList: A list possibleList of ItemChancePairs
* arraySize: An int holding the size of the above list.

In the Inspector view, the LootDropper will display arraySize. Changing this value will change the number of Item and Drop Rate fields shown. 
Here, you can drag the prefab of an Item into the Item field and input its corresponding Drop Rate. The Drop Rate is a percentage; An item with 
a Drop Rate of one will have a 1% chance of being spawned. The sum of Drop Rates should add up to 100 or less; if your sum exceeds this, your 
Item may not be spawned.

