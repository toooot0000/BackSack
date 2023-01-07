using Components.Effects;
using Components.TileObjects;

namespace Components.Ground.Triggers{
    public interface IOnTileObjectExit: IGroundTrigger{
        public IEffect OnTileObjectExit(Models.Ground.Ground ground, ITileObjectModel tileObject);
    }
}