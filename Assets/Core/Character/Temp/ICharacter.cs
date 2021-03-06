﻿using UnityEngine;
namespace GDG
{
public interface ICharacter : IDamagee, IHealee
{
    ICharacterStats GetCharacterStats(); // not needed for now
    void PickUp(IItem item);
    void EquipAsWeapon(IItem item);
    bool GetFacingDirection();
}
}