using Components.Effects;

namespace Components.Buffs.Triggers{
    public interface IOnRemoved : IBuffTrigger{
        IEffect OnRemoved(IBuffHolderModel holder);
    }
}