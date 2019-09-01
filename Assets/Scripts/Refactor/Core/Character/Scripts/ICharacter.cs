using UnityEngine;
namespace GDG
{
public interface ICharacter
{
    ICharacterStats GetCharacterStats();
    void PickUp(IItem item);
    void PickUpAsWeapon(IItem item);
    void KnockBack(Vector3 impulse);
}
}