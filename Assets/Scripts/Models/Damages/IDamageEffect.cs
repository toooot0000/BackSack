using Models.EffectInfo;
using IAttackEffect = Models.Attacks.IAttackEffect;

namespace Models.Damages{
    public interface IDamageEffect: IEffect, IAttackEffect{
        IDamageable Source{ get; }
        IDamageable Target{ get; }
        Damage Damage{ get; }
    }
}