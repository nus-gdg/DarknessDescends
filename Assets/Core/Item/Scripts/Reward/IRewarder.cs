using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
public interface IRewarder // NOTE: Entity provides rewards
{
    int GetRewardScore();
}
}
