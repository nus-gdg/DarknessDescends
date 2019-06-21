using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public virtual bool TryToAttack() { return false; }
    public virtual void StopAttack() {}
}