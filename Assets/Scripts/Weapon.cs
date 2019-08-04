using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public virtual bool TryToAttack() { return false; }
    public virtual void StopAttack() {}
    public virtual void AttachToTransform(Transform handTransform, bool isFacingRight) {
        transform.parent = handTransform;
        transform.position = handTransform.position;
        Vector3 weaponScale = transform.localScale;
        float xSign = isFacingRight ? 1 : -1;
        transform.localScale =
            new Vector3(weaponScale.x * xSign,
                        weaponScale.y,
                        weaponScale.z);
    }
}