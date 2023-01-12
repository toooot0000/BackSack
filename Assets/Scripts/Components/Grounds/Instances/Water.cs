using System;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Buffs.Instances;
using Components.Effects;
using Components.Grounds.Reducer;
using Components.Grounds.Triggers;
using Components.TileObjects;

namespace Components.Grounds.Instances{
    public class Water : IReducer, IOnTileObjectEnter{
        private static GroundType TakeElement(ElementType element){
            return element switch{
                ElementType.Earth => GroundType.Oil,
                ElementType.Electric => GroundType.Water,
                ElementType.Fire => GroundType.Steam,
                ElementType.Physic => GroundType.Water,
                ElementType.Poison => GroundType.Poison,
                ElementType.Real => GroundType.Water,
                ElementType.Water => GroundType.Water,
                ElementType.Wind => GroundType.Null,
                _ => throw new ArgumentOutOfRangeException(nameof(element), element, null)
            };
        }
        
        public IEffect OnTileObjectEnter(Ground ground, ITileObject tileObject){
            if (tileObject is IBuffHolder holder){
                return new ChangeBuffEffect<WetBuff>(holder, 2){
                    Source = ground,
                    Target = tileObject
                };
            }
            return null;
        }

        public IEffect TakeElement(Ground ground, ElementType element){
            ground.SetType(TakeElement(element));
            return null;
        }
    }
}