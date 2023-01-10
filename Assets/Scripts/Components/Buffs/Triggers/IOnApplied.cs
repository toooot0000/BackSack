using Components.Effects;

namespace Components.Buffs.Triggers{
    public interface IOnApplied : IBuffTrigger{
        IEffect OnApplied(IBuffHolder holder);
    }
}