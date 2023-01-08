using Components.Effects;
using Components.TileObjects;

namespace Components.Ground.Triggers{
    public interface IOnTurnEnd : IGroundTrigger{
        public IEffect OnTurnEnd(Ground ground, ITileObjectModel tileObject);
    }
}