using UnityEngine;

public class PlatformEdgeDemarcator : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        InteractsWithPlatformEdgeDemarcator interactsWithPlatformEdgeDemarcator = null;

        if((interactsWithPlatformEdgeDemarcator = other.gameObject.GetComponent<InteractsWithPlatformEdgeDemarcator>()) != null)
        {
            interactsWithPlatformEdgeDemarcator.InteractWithPlatformEdgeDemarcator();
        }
    }
}