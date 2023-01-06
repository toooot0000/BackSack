using Models.Buffs.Effects;
using Models.Buffs.Effects.Interfaces;

namespace Models.Buffs.Triggers{
    public interface IOnApplied : IBuffTrigger{
        IBuffEffect OnApplied(IBuffHolder holder);
    }
}