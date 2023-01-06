namespace Models.Buffs.Effects.Interfaces{
    public interface IRemoveBuffEffect<TBuff> : IBuffEffect where TBuff: Buff{
        IBuffHolder Holder{ get; }
        int RemovedLayerNumber{ get; }
    }
}