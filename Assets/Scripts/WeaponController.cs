using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WeaponController : MonoBehaviour
{
    public Transform MeleeWeaponTransform;

    //Changes
    public Transform RangedWeaponTransform;
    //Changes

    public Weapon WieldedWeapon;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Input.GetKey(KeyCode.Z)) return;
        Loot loot = collision.gameObject.GetComponent<Loot>();
        if (loot != null && loot.isWeapon())
        {
            if (WieldedWeapon != null)
            {
                Destroy(WieldedWeapon.gameObject);
            }
            WieldedWeapon = null;

            Weapon drop = (Weapon)loot.ItemDrop;
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

            //Changes
            if(drop is RangedWeapon)
            {
                drop.transform.parent = RangedWeaponTransform;
                drop.transform.position = MeleeWeaponTransform.position;
                Vector3 weaponScale = drop.transform.localScale;
                drop.transform.localScale =
                    new Vector3(weaponScale.x * Mathf.Sign(transform.localScale.x),
                                weaponScale.y,
                                weaponScale.z);

                ((RangedWeapon)drop).characterAnimator = GetComponent<CharacterAnimator>();
            }
            //Changes

            loot.ItemDrop = null;
            WieldedWeapon = drop;
            Destroy(loot.gameObject);

            SoundController.theController.playSound(SoundController.theController.pickup);
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
