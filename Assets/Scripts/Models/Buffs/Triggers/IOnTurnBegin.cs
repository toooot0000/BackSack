using Models.Buffs.Effects;
using Models.Buffs.Effects.Interfaces;

namespace Models.Buffs.Triggers{
    public interface IOnTurnBegin : IBuffTrigger{
        IBuffEffect OnTurnBegin(IBuffHolder holder);
    }
}