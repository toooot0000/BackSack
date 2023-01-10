using Components.Effects;
using Components.TileObjects;

namespace Components.Grounds.Triggers{
    public interface IOnTileObjectExit: IGroundTrigger{
        public IEffect OnTileObjectExit(Grounds.Ground ground, ITileObject tileObject);
    }
}