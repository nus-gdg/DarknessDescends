using UnityEngine;


namespace GDG
{
public class GameLogicManager : MonoBehaviour
{
    int score = 0;
    int enemiesDefeated = 0;
    int highScore = 0;

    void Start()
    {
        UIManager.Instance.UpdateScore(score);
        UIManager.Instance.UpdateHighScore(highScore);
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void OnEnemyDeath(IRewarder reward)
    {
        enemiesDefeated++;
        IncreaseScore(reward.GetRewardScore());
    }

    public void SetHighScore(int amount)
    {
        highScore = amount;
        UIManager.Instance.UpdateHighScore(highScore);
    }

    void IncreaseScore(int amount)
    {
        score += amount;
        if (score > highScore)
        {
            SetHighScore(score);
        }
        UIManager.Instance.UpdateScore(score);
    }
}
}