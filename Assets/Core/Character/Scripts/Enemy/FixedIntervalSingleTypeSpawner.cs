using System;
using UnityEngine;

namespace GDG
{
public class FixedIntervalSingleTypeSpawner : EnemySpawner
{
    float totalTimeElapsed;
    System.Random random = new System.Random();

    [SerializeField]
    public GameObject enemyToSpawn;

    [SerializeField]
    public float intervalBetweenSpawns;

    private bool isGameRunning = false;

    void Start()
    {
        EventManager.Instance.AddListener<GameStartEvent>(OnGameStart);
        EventManager.Instance.AddListener<GameOverEvent>(OnGameOver);
    }

    void OnDestroy()
    {
        EventManager.Instance.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.Instance.RemoveListener<GameOverEvent>(OnGameOver);
    }

    void Update()
    {
        if (isGameRunning)
        {
            ReceiveTimePassed(Time.deltaTime);
        }
    }

    void OnGameStart(GameStartEvent e)
    {
        isGameRunning = true;
    }

    void OnGameOver(GameOverEvent e)
    {
        isGameRunning = false;
    }

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
}