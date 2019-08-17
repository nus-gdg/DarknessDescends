using UnityEngine;

public abstract class EnemySpawner : MonoBehaviour
{
    public abstract void ReceiveTimePassed(float timeElapsedFromPreviousFrame);
    public abstract bool getStartingDirection();
    public abstract void SpawnEnemy();
}