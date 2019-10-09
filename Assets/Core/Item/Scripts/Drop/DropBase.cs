using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
    [RequireComponent(typeof(Collider2D))]
    public class DropBase : MonoBehaviour
    {
        [SerializeField]
        private float DespawnTime = 5;
        public float currentDespawnTime { get; private set; }
        public IItem itemDrop { get; set; } // assume we have an Item as a child game object

        void Start()
        {
            itemDrop = GetComponentInChildren<IItem>();
            currentDespawnTime = DespawnTime;
        }

        void FixedUpdate()
        {
            currentDespawnTime -= Time.fixedDeltaTime;
            if (currentDespawnTime <= 0f)
            {
                Destroy(gameObject);
            }
        }

        public IItem TakeItem()
        {
            currentDespawnTime = 0;
            return itemDrop;
        }

    }

}
