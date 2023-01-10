using System;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Buffs.Instances;
using Components.Effects;
using Components.Grounds.Reducer;
using Components.Grounds.Triggers;
using Components.TileObjects;

namespace Components.Grounds.Instances{
    public class Fire : IReducer, IOnTileObjectEnter{
        public GroundType TakeElement(ElementType element){
            return element switch{
                ElementType.Earth => GroundType.Explosion,
                ElementType.Electric => GroundType.Fire,
                ElementType.Fire => GroundType.Fire,
                ElementType.Physic => GroundType.Fire,
                ElementType.Poison => GroundType.Explosion,
                ElementType.Real => GroundType.Fire,
                ElementType.Water => GroundType.Steam,
                ElementType.Wind => GroundType.Fire,
                _ => throw new ArgumentOutOfRangeException(nameof(element), element, null)
            };
        }

        public IEffect OnTileObjectEnter(Grounds.Ground ground, ITileObject tileObject){
            if (tileObject is IBuffHolder holder){
                return new ChangeBuffEffect<BurningBuff>(holder, 2);
            }
            return null;
        }
    }
}