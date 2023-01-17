using Components.Effects;

namespace Components.TileObjects.StepOverAbles{
    public interface IStepOverAble: ITileObject{
        IEffectTemplate OnSteppedOver();
    }
}