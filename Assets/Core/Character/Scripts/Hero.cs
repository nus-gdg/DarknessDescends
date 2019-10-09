using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
    public class Hero : Character, IHero
    {
        void start() {}
        public override void KillAndDestroy(CharacterDeathReason reason)
        {
            GameplayController.Instance.OnHeroDeath(this);
            Destroy(gameObject);
        }

    }
}
