namespace Components.Effects{
    public interface IEffectTypedSource<out T> : IEffect{
        T Source{ get; }
    }
}