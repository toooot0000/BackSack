namespace Models.Attacks{
    public interface IAttackTarget{
        bool CanBeTargetBy(IAttackAttempt attackAttempt);
    }
}