using Components.Effects;

namespace Components.Buffs.Triggers{
    public interface IOnTakeDamage : IBuffTrigger{
        IEffect OnTakeDamage(IBuffHolder holder);
    }
}