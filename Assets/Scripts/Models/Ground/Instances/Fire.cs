using Models.Buffs;
using Models.Buffs.Effects;
using Models.Buffs.Instances;
using Models.EffectInfo;
using Models.Ground.Reducer;
using Models.Ground.Triggers;
using Models.TileObjects;

namespace Models.Ground.Instances{
    public class Fire : IReducer, IOnTileObjectEnter{
        public GroundType TakeElement(ElementType element){
            return element switch{
                ElementType.Earth => GroundType.Explosion,
                ElementType.Electric => GroundType.Fire,
                ElementType.Fire => GroundType.Fire,
                ElementType.Physic => GroundType.Fire,
                ElementType.Poison => GroundType.Explosion,
                ElementType.Real => GroundType.Fire,
                ElementType.Water => GroundType.Steam
            };
        }

        public IEffect OnTileObjectEnter(Ground ground, ITileObject tileObject){
            if (tileObject is IBuffHolder holder){
                return new AddBuffEffect<BurningBuff>(){
                    Holder = holder,
                    AddLayerNumber = 1
                };
            }
            return null;
        }
    }
}