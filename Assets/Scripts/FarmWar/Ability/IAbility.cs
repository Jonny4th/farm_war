public interface IAbility
{
    public int Cost { get; }
    public bool IsReady { get; }
    public void Execute();
}
