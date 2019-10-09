using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GDG
{
using static GameManager;
public class UIManager : Manager<UIManager>
{
    public Text scoreDisplay;
    public Text highScoreDisplay;
    public Text timeDisplay;
    public GameObject endgameDisplay;
    public GameObject pauseMenu;

    public void UpdateScore(int newScore)
    {
        scoreDisplay.text = newScore.ToString();
    }

    public void UpdateHighScore(int highScore)
    {
        highScoreDisplay.text = highScore.ToString();
    }

    public void DisplayEndGame()
    {
        endgameDisplay.SetActive(true);
    }

    public void UpdateTime(float time)
    {
        timeDisplay.text = time.ToString();
    }

    public void TogglePauseMenu(bool display)
    {
        pauseMenu.SetActive(display);
    }
}
}
