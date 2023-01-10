using Components.Damages;
using Components.Effects;

namespace Components.Buffs.Triggers{
    public interface IOnConsumeDamage : IBuffTrigger{
        IEffect OnConsumeDamageEffect(IBuffHolder holder, IDamageEffect effect);
    }
}