using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
[RequireComponent(typeof(Collider2D))]
public class Hero : Character, IHero
{
    private Collider2D looterCollider;
    public override void KillAndDestroy(CharacterDeathReason reason)
    {
        GameplayController.Instance.OnHeroDeath(this);
        Destroy(gameObject);
    }

    public Collider2D GetLooterCollider()
    {
        if (looterCollider == null)
            looterCollider = GetComponent<Collider2D>();
        return looterCollider;
    }

}
}
