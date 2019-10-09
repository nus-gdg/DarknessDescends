# Item
The Item class is a catch-all abstract class that represents everything that can be dropped as loot. This includes all weapons and power-ups.

# Weapon
See [Weapon.md](Weapon.md).

# WeaponController
The OnTriggerEnter2D and OnTriggerExit2D methods take note of the Weapons that the Player is currently able to pick up. When the Player presses the
key mapped to Weapon Pickups, the Pickup method is called. This will destroy any Weapon the Player is currently holding and attach the new Weapon to
the Transform.

The QueryOldestWeapon method exists to establish a logic for when the Player is able to pick up more than one Weapon. It returns a reference to the
weapon that will expire the soonest.

# PowerUp
The PowerUp is an abstract class. Unlike Weapons, their effect should be triggered upon contact with the Player Character and should not require the
player to actively pick them up. As such their implementation of `PickUpWhenInRange` should return `true`.
```cs
    public bool PickUpWhenInRange()
    {
        return true;
    }
```

# Healthpack
The Healthpack represents a basic implementation of a PowerUp. Its implementation of "OnPickup" does two things: it heals the Player by
an amount set by the developer, and then destroys itself.
