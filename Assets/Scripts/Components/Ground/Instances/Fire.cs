using System;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Buffs.Instances;
using Components.Effects;
using Components.Ground.Reducer;
using Components.Ground.Triggers;
using Components.TileObjects;
using Models;
using Models.Ground;

namespace Components.Ground.Instances{
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

        public IEffect OnTileObjectEnter(Models.Ground.Ground ground, ITileObjectModel tileObject){
            if (tileObject is IBuffHolderModel holder){
                return new ChangeBuffEffect<BurningBuff>(holder, 2);
            }
            return null;
        }
    }
}