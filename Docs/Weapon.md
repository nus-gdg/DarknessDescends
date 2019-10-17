# Weapon

A Weapon implements the `IItem` interface and can be used by Characters who have picked it up.

# Implementing my Weapon

## Set Up

Create your weapon script. For this example we will name it `SwingingWeapon.cs` We can use inherit from `Weapon` which already implements pickup behaviour which attachs the weapon to the Character's transform. The `Use()` method will be called whenever the Character wants to use the weapon. We will be explaining how to implement a melee swinging weapon.

Create a `GameObject` with has a `Collider2D` (Box, Circle) and a `Damager` script. You can think of Damagers as Hit Boxes which apply damage to characters on collision. We will use our `Collider2D` to detect collisions and the `Damager` component applies damage to intersecting Characters.
Naturally we will need some visuals, you can use a `SpriteRenderer` with an appropriate sprite.

**NOTE:** 
Disable the `Collider2D`, we will be using our weapon script to disable or enable the collider. Also, make sure you make your collider a `trigger` collider.

```cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDG;

public class SwingingWeapon : Weapon
{
    public float SwingTime = 1;
    bool isSwinging = false;
    public override void Use()
    {
        if (!isSwinging)
        {
            StartCoroutine(Swing());
        }
    }

    IEnumerator Swing()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = true;
        isSwinging = true;

        float timeElapsed = 0;
        while (timeElapsed < SwingTime)
        {
            timeElapsed += Time.deltaTime;
            float angle = timeElapsed / SwingTime * 90;
            transform.localRotation = Quaternion.Euler(0, 0, -angle);
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = Quaternion.identity;
        col.enabled = false;
        isSwinging = false;
    }
}
```

This script uses [Coroutines](https://docs.unity3d.com/Manual/Coroutines.html). If the weapon isn't swinging we will execute our Swing coroutine which does the following:
1. Turns on the `Collider2D` so that damage can be dealt
2. Rotates it self over a set `SwingTime`
3. Turns off the collider when it has finished the swing.

## Testing

When testing the weapon in the `Main` scene, we can't directly attach it to the `Hero` character. We will have to place our weapon `GameObject` as a child of the `Drop Base` prefab. This will allow it to be picked up. Now when you start the game, you can pick up the weapon with `Z` and swing with `E`.


# Damager

A damager interacts with the Character to cause the character to lose health. Damage is intended to be given to entities that will come into contact with the Character, such as projectiles and swords. For this to work, both the Character and the Damager must have a 2D collider attached to it. The character should have a rigid body. The damage exposes the following functionality in the unity editor:

* Damage amount: The damage the character will take
* Damage Source: Either `Friendly` or `Enemy`.
* Force: push-back for the opponent when it takes damage.

Additionally, the damager script can be extended. The _virtual_ method ApplyDamage is called when the damager comes into contact with a `Character`. By default, it applies damage. However, it can be extended to be destroyed (in the case of projectiles) or fork (for special projectile attacks).
