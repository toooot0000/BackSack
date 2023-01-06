using Models.Buffs.Effects.Interfaces;

namespace Models.Buffs.Effects{
    public class RemoveBuffEffect<TBuff>: IRemoveBuffEffect<TBuff> where TBuff: Buff{
        public IBuffHolder Holder{ set; get; }
        public int RemovedLayerNumber{ set; get; }
    }
}