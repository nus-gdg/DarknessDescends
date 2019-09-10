using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
public interface IItem
{
    void Use();
    void OnPickUp(ICharacter character);
    void SetParentTransform(Transform t);
    void SetFaceDirection(bool faceRight);
    void Destroy();
}
}