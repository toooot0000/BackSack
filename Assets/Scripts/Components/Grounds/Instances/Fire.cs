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
        public IEffect TakeElement(Ground ground, ElementType element){
            switch (element){
                case ElementType.Fire:
                    return null;
                case ElementType.Water:
                    ground.SetType(GroundType.Null);
                    return null;
                case ElementType.Wind:
                    return null;
                case ElementType.Earth:
                    return null;
                case ElementType.Electric:
                    return null;
                case ElementType.Poison:
                    return null;
                case ElementType.Physic:
                    return null;
                case ElementType.Real:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(element), element, null);
            }
        }

        public IEffect OnTileObjectEnter(Ground ground, ITileObject tileObject){
            if (tileObject is IBuffHolder holder){
                return new ChangeBuffEffect<BurningBuff>(holder, 2){
                    Source = ground,
                    Target = tileObject
                };
            }
            return null;
        }
    }
}