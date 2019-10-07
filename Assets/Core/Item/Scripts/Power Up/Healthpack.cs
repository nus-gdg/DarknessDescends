using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
public class Healthpack : PowerUp
{
    public float healAmount;
    public override void OnPickUp(ICharacter character)
    {
        character.Heal(healAmount);
        SoundManager.Instance.PlayPickup();
        Destroy(gameObject.transform.root.gameObject);
    }
}
}
