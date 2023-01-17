using Components.Attacks;

namespace Components.Enemies{
    public interface IEnemyAttack: IAttack{
        IAttackAnimator GetAnimator();
    }
}