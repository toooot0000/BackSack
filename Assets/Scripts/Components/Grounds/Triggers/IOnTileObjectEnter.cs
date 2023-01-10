using Components.Effects;
using Components.TileObjects;

namespace Components.Grounds.Triggers{
    public interface IOnTileObjectEnter: IGroundTrigger{
        public IEffect OnTileObjectEnter(Grounds.Ground ground, ITileObject tileObject);
    }
}