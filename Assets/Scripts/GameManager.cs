using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int score = 0;
    int enemiesDefeated = 0;
    float timeSinceGameStart = 0.0f;
    bool playerDead = false;

    public Text scoreDisplay;
    public Text timeDisplay;
    public Text endgameDisplay;

    void Awake()
    {
        endgameDisplay.enabled = false;
    }

    void Update ()
    {
        float timeElapsedFromPreviousFrame = Time.deltaTime;
        timeSinceGameStart += timeElapsedFromPreviousFrame;
        sendSignalToSpawnPoints(timeElapsedFromPreviousFrame);

        scoreDisplay.text = score.ToString();
        timeDisplay.text = timeSinceGameStart.ToString();
    }

    void sendSignalToSpawnPoints(float timeElapsedFromPreviousFrame)
    {
        /*
         * I think it makes more sense for each spawner to be in-charge of its own spawning.
         * The GameManager will track how much time has progressed, and send this
         * info to the spawners. Whenever enough time has elapsed in each spawner,
         * it will shoot out the next enemy and reset the time that has passed.
         */
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