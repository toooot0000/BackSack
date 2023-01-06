using Models.Buffs.Effects;
using Models.Buffs.Effects.Interfaces;

namespace Models.Buffs.Triggers{
    public interface IOnTurnEnd : IBuffTrigger{
        IBuffEffect OnTurnEnd(IBuffHolder holder);
    }
}