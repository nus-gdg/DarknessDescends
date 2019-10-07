using UnityEngine;

namespace GDG
{
[RequireComponent(typeof(Collider2D))]
public class PowerUp : MonoBehaviour, IItem
{
    public virtual void OnPickUp(ICharacter character) { }
    public virtual void Use() { }
    public void SetParentTransform(Transform transform) { }
    public void Destroy()
    {
        Destroy(gameObject.transform.root);
    }

    public bool PickUpWhenInRange()
    {
        return true;
    }
}
}