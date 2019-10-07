using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
public class GameManager : Manager<GameManager>
{
    public enum GameState
    {
        Pregame,
        Running,
        Paused,
        Postgame
    }
    public GameState CurrentGameState { get; set; } = GameState.Pregame;

    void Start()
    {
        UpdateState(GameState.Running);
    }

    void Update()
    {
        if (CurrentGameState == GameState.Pregame)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void UpdateState(GameState state)
    {
        GameState previousGameState = CurrentGameState;
        CurrentGameState = state;

        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (CurrentGameState)
        {
            case GameState.Pregame:
                Time.timeScale = 1.0f;
                break;
            case GameState.Running:
                Time.timeScale = 1.0f;
                break;
            case GameState.Paused:
                Time.timeScale = 0.0f;
                break;
        }
        EventManager.Instance.Raise(new GameStateChangedEvent
            {
                currentGameState = CurrentGameState,
                previousGameState = previousGameState
            }
        );
    }

    public void TogglePause()
    {
        UpdateState(CurrentGameState == GameState.Running ?
            GameState.Paused : GameState.Running);
    }

    public void RestartGame()
    {
        UpdateState(GameState.Pregame);
    }
}

public class GameStateChangedEvent : GameEvent
{
    public GameManager.GameState currentGameState;
    public GameManager.GameState previousGameState;
}
}