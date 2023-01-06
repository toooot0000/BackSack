using Models.Buffs.Effects;
using Models.Buffs.Effects.Interfaces;

namespace Models.Buffs.Triggers{
    public interface IOnRemoved : IBuffTrigger{
        IBuffEffect OnRemoved(IBuffHolder holder);
    }
}