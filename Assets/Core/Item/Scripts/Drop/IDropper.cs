using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDG
{
public interface IDropper // NOTE: entity that implements this will drop items
{
    List<DropChancePair> GetDropList();
    Vector3 GetDropPosition();
}
}
