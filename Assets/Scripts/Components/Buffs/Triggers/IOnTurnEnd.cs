using Components.Effects;

namespace Components.Buffs.Triggers{
    public interface IOnTurnEnd : IBuffTrigger{
        IEffect OnTurnEnd(IBuffHolderModel holder);
    }
}