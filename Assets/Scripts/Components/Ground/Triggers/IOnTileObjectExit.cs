using Components.Effects;
using Components.TileObjects;

namespace Components.Ground.Triggers{
    public interface IOnTileObjectExit: IGroundTrigger{
        public IEffect OnTileObjectExit(Ground ground, ITileObjectModel tileObject);
    }
}