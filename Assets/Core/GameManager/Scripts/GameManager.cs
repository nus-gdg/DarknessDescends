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
    public GameObject pauseMenu;

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

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
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

    public void OnCharacterDeath(Character character, CharacterDeathReason reason)
    {
        if(character.tag == "Player")
        {
            playerCharacterDies();
        }
        else if (reason == CharacterDeathReason.KILLED_BY_DAMAGE) // record deaths if killed by player
        {
            // enemyDies(character.rewardUponDeath);
        }
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

    public void Pause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void ToggleSound()
    {
        Text muteButtonText = pauseMenu.transform.Find("Mute Button/Text").GetComponent<Text>();
        if (AudioListener.volume == 0f)
        {
            AudioListener.volume = 1f;
            muteButtonText.text = "Mute";
        } else
        {
            AudioListener.volume = 0f;
            muteButtonText.text = "Unmute";
        }
    }
}