using Components.Effects;

namespace Components.Buffs.Triggers{
    public interface IOnTurnEnd : IBuffTrigger{
        IEffect OnTurnEnd(IBuffHolder holder);
    }
}