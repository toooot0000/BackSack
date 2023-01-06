using Models.Buffs.Effects.Interfaces;

namespace Models.Buffs.Effects{
    public class AddBuffEffect<TBuff>: IAddBuffEffect<TBuff> where TBuff: Buff{
        public IBuffHolder Holder{ set; get; }
        public int AddLayerNumber{ set; get; }
    }
}