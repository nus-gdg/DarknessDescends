using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
public abstract class Weapon : MonoBehaviour, IItem
{
    public virtual void OnPickUp(ICharacter c)
    {
        c.EquipAsWeapon(this);
        setFaceDirection(c.GetFacingDirection());
    }

    public void SetParentTransform(Transform t)
    {
        transform.parent = t;
        transform.position = t.position;
    }

    private void setFaceDirection(bool facingRight)
    {
        Vector3 weaponScale = transform.localScale;
        float xSign = facingRight ? 1 : -1; // To Change
        transform.localScale =
            new Vector3(weaponScale.x * xSign,
                        weaponScale.y,
                        weaponScale.z);
    }


    public void Destroy()
    {
        Destroy(gameObject);
    }

    public bool PickUpWhenInRange()
    {
        return false;
    }

    public abstract void Use();
}
}
