using Models.EffectInfo;
using Models.TileObjects;

namespace Models.Ground.Triggers{
    public interface IOnTileObjectExit: IGroundTrigger{
        public IEffect OnTileObjectExit(Ground ground, ITileObject tileObject);
    }
}