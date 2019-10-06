using UnityEngine;
using GDG;

public abstract class PowerUp : Item
{
    public abstract void InteractWithPowerUp(Character playerCharacter);
}