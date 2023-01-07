using Components.Effects;

namespace Components.Buffs.Effects{
    public interface IBuffEffect : IEffectTypedTarget<IBuffHolderModel>{
        int ChangeNumber{ get; }
        void Apply();

        string GetDisplayName();
    }
}