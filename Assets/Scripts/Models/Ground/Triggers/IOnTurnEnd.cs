using Models.EffectInfo;
using Models.TileObjects;

namespace Models.Ground.Triggers{
    public interface IOnTurnEnd : IGroundTrigger{
        public IEffect OnTurnEnd(Ground ground, ITileObject tileObject);
    }
}