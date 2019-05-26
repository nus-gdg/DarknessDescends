using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WeaponController : MonoBehaviour
{
    public Transform MeleeWeaponTransform;
    public Weapon WieldedWeapon;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Input.GetKey(KeyCode.Z)) return;
        Loot loot = collision.gameObject.GetComponent<Loot>();
        if (loot != null && loot.WeaponDrop != null)
        {
            if (WieldedWeapon != null)
            {
                Destroy(WieldedWeapon.gameObject);
            }
            WieldedWeapon = null;

            Weapon drop = loot.WeaponDrop;;
            if (drop is MeleeWeapon)
            {
                drop.transform.parent = MeleeWeaponTransform;
                drop.transform.position = MeleeWeaponTransform.position;
                Vector3 weaponScale = drop.transform.localScale;
                drop.transform.localScale =
                    new Vector3(weaponScale.x * Mathf.Sign(transform.localScale.x),
                                weaponScale.y,
                                weaponScale.z);
            }
            loot.WeaponDrop = null;
            WieldedWeapon = drop;
            Destroy(loot.gameObject);
        }
    }

    void Update() {
        if (WieldedWeapon == null) return;
        if (Input.GetKey(KeyCode.E)) {
            WieldedWeapon.TryToAttack();
        } else {
            WieldedWeapon.StopAttack();
        }
    }
}
