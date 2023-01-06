namespace Models.Buffs.Effects.Interfaces{
    public interface IAddBuffEffect<TBuff>: IBuffEffect where TBuff: Buff{
        IBuffHolder Holder{ get; }
        int AddLayerNumber{ get; }
    }
}