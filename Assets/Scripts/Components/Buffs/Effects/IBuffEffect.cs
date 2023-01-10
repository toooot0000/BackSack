using Components.Effects;

namespace Components.Buffs.Effects{
    public interface IBuffEffect : IEffect{
        int ChangeNumber{ get; }
        void Apply();
        string GetDisplayName();
    }
}