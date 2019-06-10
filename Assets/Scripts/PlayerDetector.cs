using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public PlayerDetectedEvent playerDetectedEvent;

    void Start()
    {
        if(playerDetectedEvent == null)
        {
            playerDetectedEvent = new PlayerDetectedEvent();
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        playerDetectedEvent.Invoke();
    }
}
