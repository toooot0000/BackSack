using Components.Effects;

namespace Components.Buffs.Triggers{
    public interface IOnTurnBegin : IBuffTrigger{
        IEffect OnTurnBegin(IBuffHolderModel holder);
    }
}