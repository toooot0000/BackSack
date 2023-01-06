using Models.Buffs.Effects;
using Models.Buffs.Effects.Interfaces;

namespace Models.Buffs.Triggers{
    public interface IOnGetResistance : IBuffTrigger{
        IBuffEffect OnGetResistance(IBuffHolder holder);
    }
}