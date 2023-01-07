using Components.Effects;
using Components.TileObjects;

namespace Components.Ground.Triggers{
    public interface IOnTurnEnd : IGroundTrigger{
        public IEffect OnTurnEnd(Models.Ground.Ground ground, ITileObjectModel tileObject);
    }
}