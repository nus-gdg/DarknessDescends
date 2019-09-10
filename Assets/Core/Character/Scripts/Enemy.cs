using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
public class Enemy : Character, IEnemy
{
    public List<DropChancePair> DropList;
    public int RewardScore;

    public List<DropChancePair> GetDropList()
    {
        return DropList;
    }

    public Vector3 GetDropPosition()
    {
        return transform.position;
    }

    public int GetRewardScore()
    {
        return RewardScore;
    }

    public override void KillAndDestroy(CharacterDeathReason reason)
    {
        GameplayController.Instance.OnEnemyDeath(this);
        Destroy(gameObject);
    }
}
}
