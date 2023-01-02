using Models.Buffs;
using Models.Buffs.BuffInstances;
using Models.TileObjects;

namespace Models.GroundEffects{
    public class FireEffect : IGroundEffect, IGroundEffectOnTileObjectEnter{
        public GroundEffectType TakeElement(ElementType element){
            return element switch{
                ElementType.Earth => GroundEffectType.Explosion,
                ElementType.Electric => GroundEffectType.Fire,
                ElementType.Fire => GroundEffectType.Fire,
                ElementType.Physic => GroundEffectType.Fire,
                ElementType.Poison => GroundEffectType.Explosion,
                ElementType.Real => GroundEffectType.Fire,
                ElementType.Water => GroundEffectType.Steam
            };
        }

        public void OnTileObjectEnter(BaseGroundEffect groundEffect, ITileObject tileObject){
            if (tileObject is IBuffHolder holder){
                holder.AddBuffLayer<BurningBuff>(2);
            }
        }
    }
}