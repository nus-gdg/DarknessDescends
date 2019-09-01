namespace GDG
{
public interface ICharacterStats
{
    float GetCurrentHealth();
    float GetTotalHealth();
    float GetCurrentSpeed();

    void Damage(float amount);
    void Heal(float amount);
    void UpdateSpeed(float amount);
}
}
