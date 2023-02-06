using Components.Effects;
using Components.TileObjects.Effects;

namespace Components.TileObjects.ForceMovables{
    public interface IForceMovable : ITileObject, ICanConsume<IForceMovement>{
        int Weight{ set; get; }
    }
}