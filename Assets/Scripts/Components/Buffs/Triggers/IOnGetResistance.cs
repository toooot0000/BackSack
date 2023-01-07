using Components.Effects;

namespace Components.Buffs.Triggers{
    public interface IOnGetResistance : IBuffTrigger{
        IEffect OnGetResistance(IBuffHolderModel holder);
    }
}