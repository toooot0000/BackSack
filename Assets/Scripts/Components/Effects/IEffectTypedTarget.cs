namespace Components.Effects{
    public interface IEffectTypedTarget<out T>: IEffect{
        T Target{ get; }
    }
}