using Components.Damages;
using Components.Effects;

namespace Components.Buffs.Triggers{
    public interface IBeforeConsumeDamage : IBuffTrigger{
        IEffect BeforeConsumeDamage(IBuffHolder holder, IDamage effect);
    }
}