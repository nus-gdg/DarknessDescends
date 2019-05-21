using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    int score = 0;
    int enemiesDefeated = 0;
    float timeSinceGameStart = 0.0f;
    bool playerDead = false;

    public Text scoreDisplay;
    public Text timeDisplay;
    public Text endgameDisplay;

    public List <GameObject> spawnerList;

    void Awake()
    {
        endgameDisplay.enabled = false;
    }

    void Update ()
    {
        float timeElapsedFromPreviousFrame = Time.deltaTime;
        timeSinceGameStart += timeElapsedFromPreviousFrame;
        SendSignalToSpawnPoints(timeElapsedFromPreviousFrame);

        scoreDisplay.text = score.ToString();
        timeDisplay.text = timeSinceGameStart.ToString();
    }

    void SendSignalToSpawnPoints(float timeElapsedFromPreviousFrame)
    {
        foreach(GameObject spawner in spawnerList)
        {
            spawner.GetComponent<EnemySpawner>().ReceiveTimePassed(timeElapsedFromPreviousFrame);
        }
    }

    public void enemyDies(int increaseInScore)
    {
        score += increaseInScore;
        enemiesDefeated++;
    }

    void playerCharacterDies()
    {
        playerDead = true;
        endgameDisplay.enabled = true;
    }
}