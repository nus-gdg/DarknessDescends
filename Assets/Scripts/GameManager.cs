using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    int score = 0;
    int highScore;
    int enemiesDefeated = 0;
    float timeSinceGameStart = 0.0f;
    bool playerDead = false;

    public Text scoreDisplay;
    public Text highScoreDisplay;
    public Text timeDisplay;
    public Text endgameDisplay;

    public List <GameObject> spawnerList;

    void Awake()
    {
        endgameDisplay.enabled = false;
        scoreDisplay.text = score.ToString();
        highScoreDisplay.text = highScore.ToString();
    }

    void Update ()
    {
        if(!playerDead){
            float timeElapsedFromPreviousFrame = Time.deltaTime;
            timeSinceGameStart += timeElapsedFromPreviousFrame;
            SendSignalToSpawnPoints(timeElapsedFromPreviousFrame);
            timeDisplay.text = timeSinceGameStart.ToString();
        }

        if(playerDead && Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void SendSignalToSpawnPoints(float timeElapsedFromPreviousFrame)
    {
        foreach(GameObject spawner in spawnerList)
        {
            spawner.GetComponent<EnemySpawner>().ReceiveTimePassed(timeElapsedFromPreviousFrame);
        }
    }

    public void increaseScore(int increaseInScore)
    {
        score += increaseInScore;

        if(highScore < score)
        {
            highScore = score;
            highScoreDisplay.text = highScore.ToString();
        }

        scoreDisplay.text = score.ToString();
    }

    public void enemyDies(int increaseInScore)
    {
        increaseScore(increaseInScore);
        enemiesDefeated++;
    }

    public void playerCharacterDies()
    {
        playerDead = true;
        endgameDisplay.enabled = true;
    }
}