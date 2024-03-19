public interface IHealable
{
    public void Heal(float healPoints);
    public bool IsHealingNeeded { get; }
}
