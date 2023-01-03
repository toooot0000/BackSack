using Models.Buffs;
using Models.Buffs.BuffInstances;
using Models.TileObjects;

namespace Models.Ground{
    public class Fire : IGround, IGroundOnTileObjectEnter{
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

        public void OnTileObjectEnter(BaseGround ground, ITileObject tileObject){
            if (tileObject is IBuffHolder holder){
                holder.AddBuffLayer<BurningBuff>(2);
            }
        }
    }
}