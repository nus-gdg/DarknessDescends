using System;
using UnityEngine;

public class FixedIntervalSingleTypeSpawner : EnemySpawner
{
    float totalTimeElapsed;
    System.Random random = new System.Random();

    [SerializeField]
    public GameObject enemyToSpawn;

    [SerializeField]
    public float intervalBetweenSpawns;

    public override void ReceiveTimePassed(float timeElapsedFromPreviousFrame)
    {
        totalTimeElapsed += timeElapsedFromPreviousFrame;

        if(totalTimeElapsed >= intervalBetweenSpawns)
        {
            totalTimeElapsed -= intervalBetweenSpawns;
            SpawnEnemy();
        }
    }

    public override bool getStartingDirection()
    {
        if(random.NextDouble() < 0.5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void SpawnEnemy()
    {
        Instantiate(enemyToSpawn, this.gameObject.transform.position, Quaternion.identity, this.transform);
    }
}