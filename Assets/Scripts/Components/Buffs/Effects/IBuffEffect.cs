using Components.Effects;

namespace Components.Buffs.Effects{
    public interface IBuffEffect : IEffectTypedTarget<IBuffHolder>{
        int ChangeNumber{ get; }
        void Apply();
        string GetDisplayName();
    }
}