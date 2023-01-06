using Models.Buffs.Effects;
using Models.Buffs.Effects.Interfaces;

namespace Models.Buffs.Triggers{
    public interface IOnTakeDamage : IBuffTrigger{
        IBuffEffect OnTakeDamage(IBuffHolder holder);
    }
}