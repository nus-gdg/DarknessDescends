using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    SavedData savedData;

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
        savedData = LoadSavedData();

        if(savedData != null)
        {
            UpdateStateBasedOnSavedData(savedData);
        }

        endgameDisplay.enabled = false;
        scoreDisplay.text = score.ToString();
        highScoreDisplay.text = highScore.ToString();
    }

    void Update()
    {
        if(!playerDead){
            float timeElapsedFromPreviousFrame = Time.deltaTime;
            timeSinceGameStart += timeElapsedFromPreviousFrame;
            SendSignalToSpawnPoints(timeElapsedFromPreviousFrame);
            timeDisplay.text = timeSinceGameStart.ToString();
        }

        if(playerDead && Input.GetKeyDown("r"))
        {
            RecordSavedData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(playerDead && Input.GetKeyDown("f"))
        {
            DeleteSavedData();
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
        SoundController.theController.playSound(SoundController.theController.gameover);
        playerDead = true;
        endgameDisplay.enabled = true;
    }

    void RecordSavedData()
    {
        savedData = new SavedData();

        savedData.highScore = highScore;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.savedData");
        bf.Serialize(file, savedData);
        file.Close();

        Debug.Log("Data saved");
    }

    SavedData LoadSavedData()
    {
        if(File.Exists(Application.persistentDataPath + "/save.savedData"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.savedData", FileMode.Open);
            SavedData returnValue = (SavedData) bf.Deserialize(file);
            file.Close();

            Debug.Log("Save data found.");

            return returnValue;
        }

        Debug.Log("Failed to load save data.");
        return null;
    }

    void UpdateStateBasedOnSavedData(SavedData incomingSaveData)
    {
        highScore = incomingSaveData.highScore;
    }

    void DeleteSavedData()
    {
        File.Delete(Application.persistentDataPath + "/save.savedData");
        Debug.Log("Save data deleted.");
    }
}