using Components.Effects;
using Components.TileObjects.Effects;

namespace Components.TileObjects.ForceMovables{
    public interface IForceMovableModel : ITileObjectModel{
        int Weight{ set; get; }
    }

    public interface IForceMovable : ITileObject, ICanConsume<IForceMovement>{ }
}