using UnityEngine;

namespace GDG
{
using static GameManager;
public class GameplayController :  Singleton<GameplayController>
{
    public GameLogicManager gameLogic;
    public DropManager dropManager;

    bool isGameRunning = false;
    float timeSinceGameStart = 0.0f;
    SaveData saveData;

    protected override void Awake()
    {
        base.Awake();
        EventManager.Instance.AddListener<GameStateChangedEvent>(OnGameStateChanged);

    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventManager.Instance.RemoveListener<GameStateChangedEvent>(OnGameStateChanged);
    }

    void OnGameStateChanged(GameStateChangedEvent e)
    {
        if (e.previousGameState == GameState.Pregame &&
            e.currentGameState == GameState.Running)
        {
            StartGame();
        }

        if (e.previousGameState == GameState.Paused &&
            e.currentGameState != GameState.Paused)
        {
            TogglePauseGame(false);
        }
        else if (e.previousGameState != GameState.Paused &&
            e.currentGameState == GameState.Paused)
        {
            TogglePauseGame(true);
        }
    }

    void StartGame()
    {
        saveData = SaveData.LoadSaveData();
        if(saveData != null)
        {
            UpdateStateBasedOnSaveData(saveData);
        }
        timeSinceGameStart = 0.0f;
        isGameRunning = true;
        EventManager.Instance.Raise(new GameStartEvent {});
    }

    void TogglePauseGame(bool toggle)
    {
        UIManager.Instance.TogglePauseMenu(toggle);
    }

    void Update()
    {
        if (isGameRunning)
        {
            float timeElapsedFromPreviousFrame = Time.deltaTime;
            timeSinceGameStart += timeElapsedFromPreviousFrame;
            UIManager.Instance.UpdateTime(timeSinceGameStart);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SaveData.DeleteSaveData();
        }
    }

    public void OnEnemyDeath(IEnemy enemy)
    {
        gameLogic.OnEnemyDeath(enemy);
        dropManager.DropReward(enemy);
    }

    public void OnHeroDeath(IHero hero)
    {
        GameOver();
    }

    void GameOver()
    {
        RecordSavedData();
        SoundManager.Instance.PlayGameOver();
        UIManager.Instance.DisplayEndGame();
        isGameRunning = false;
        EventManager.Instance.Raise(new GameOverEvent {});
    }

    void RecordSavedData()
    {
        saveData = SaveData.RecordHighscoreIntoSave(gameLogic.GetHighScore());
    }

    void UpdateStateBasedOnSaveData(SaveData incomingSaveData)
    {
        Debug.Log(incomingSaveData.highScore);
        gameLogic.SetHighScore(incomingSaveData.highScore);
    }
}

public class GameStartEvent : GameEvent { }
public class GameOverEvent : GameEvent { }
}
