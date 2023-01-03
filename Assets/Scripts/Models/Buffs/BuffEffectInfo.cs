using Models.Damageable;
using Models.EffectInfo;
using MVC;

namespace Models.Buffs{
    public abstract class BuffEffectInfo : Model{ }
    
    public interface IAddBuffEffect<TBuff>: IEffect where TBuff: Buff{
        IBuffHolder Holder{ get; }
        int AddLayerNumber{ get; }
    }

    public interface IRemoveBuffEffect<TBuff> : IEffect where TBuff: Buff{
        IBuffHolder BuffHolder{ get; }
        int RemovedLayerNumber{ get; }
    }
}