using Components.Effects;

namespace Components.TileObjects.StepOverAbles{
    /// <summary>
    /// For Items Only
    /// </summary>
    public interface IStepOverAble: ITileObject{
        IEffectTemplate OnSteppedOver();
    }
}