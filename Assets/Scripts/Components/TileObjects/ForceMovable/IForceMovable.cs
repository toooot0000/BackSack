using Components.Effects;
using Components.TileObjects.Effects;
using MVC;

namespace Components.TileObjects.ForceMovable{
    public interface IForceMovableModel : ITileObjectModel{
        int Weight{ set; get; }
    }

    public interface IForceMovable : ITileObject, ICanConsume<IForceMovement>{ }
}