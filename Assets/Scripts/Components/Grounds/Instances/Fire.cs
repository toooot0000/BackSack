using System;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Buffs.Instances;
using Components.Damages;
using Components.Effects;
using Components.Grounds.Reducer;
using Components.Grounds.Triggers;
using Components.TileObjects;

namespace Components.Grounds.Instances{
    public class Fire : IReducer, IOnTileObjectEnter{
        public IEffectTemplate TakeElement(Ground ground, ElementType element){
            switch (element){
                case ElementType.Wind:
                case ElementType.Electric:
                case ElementType.Fire:
                case ElementType.Physic:
                case ElementType.Real:
                    return null;
                
                case ElementType.Water:
                    ground.SetType(GroundType.Steam);
                    return null;
                
                case ElementType.Earth:
                case ElementType.Poison:
                    ground.SetType(GroundType.Null);
                    return new DamageEffect(ground, null, new Damage(){
                        Element = ElementType.Fire,
                        Point = 5
                    });
                
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