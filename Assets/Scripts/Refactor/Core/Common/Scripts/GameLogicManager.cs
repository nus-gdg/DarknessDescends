using UnityEngine;


namespace GDG
{
public class GameLogicManager : MonoBehaviour
{
    int score = 0;
    int enemiesDefeated = 0;
    public int highScore = 0;

    public void OnEnemyDeath(IRewarder reward)
    {
        enemiesDefeated++;
        IncreaseScore(reward.GetRewardScore());
    }

    void IncreaseScore(int amount)
    {
        // ui manager do stuff
        score += amount;
        UIManager.Instance.UpdateScore(score);
    }
}
}