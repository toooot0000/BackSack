using Models.EffectInfo;
using Models.TileObjects;

namespace Models.Ground.Triggers{
    public interface IOnTileObjectEnter: IGroundTrigger{
        public IEffect OnTileObjectEnter(Ground ground, ITileObject tileObject);
    }
}