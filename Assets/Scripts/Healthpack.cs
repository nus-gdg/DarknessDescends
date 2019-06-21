using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpack : PowerUp
{
    public int healAmount;

    public override void InteractWithPowerUp(Character playerCharacter)
    {
        playerCharacter.Heal(healAmount);
        Destroy(gameObject.transform.root.gameObject);
    }
}