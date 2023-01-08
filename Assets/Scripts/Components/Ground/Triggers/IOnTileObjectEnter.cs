using Components.Effects;
using Components.TileObjects;

namespace Components.Ground.Triggers{
    public interface IOnTileObjectEnter: IGroundTrigger{
        public IEffect OnTileObjectEnter(Ground ground, ITileObjectModel tileObject);
    }
}