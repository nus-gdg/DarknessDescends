using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
    public class Enemy : Character, IEnemy
    {
        public List<DropChancePair> DropList;
        public int dropChanceRange;
        public int RewardScore;

        IEnumerator Start()
        {
            CalculateDropChanceRange();
            return null;
        }
        private void CalculateDropChanceRange()
        {
            foreach(DropChancePair p in DropList)
            {
                dropChanceRange += p.weightage;
            }
        }
        public int GetDropChanceRange()
        {
            return dropChanceRange;
        }
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
