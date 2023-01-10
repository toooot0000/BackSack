using Components.Effects;
using Components.TileObjects;

namespace Components.Grounds.Triggers{
    public interface IOnTurnEnd : IGroundTrigger{
        public IEffect OnTurnEnd(Ground ground, ITileObject tileObject);
    }
}