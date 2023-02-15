using System;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Buffs.Instances;
using Components.Effects;
using Components.Grounds.Reducer;
using Components.Grounds.Triggers;
using Components.TileObjects;

namespace Components.Grounds.Instances{
    public class Steam : IReducer, IOnTileObjectEnter, IOnTurnEnd, IOnTileObjectExit{

        public IEffectTemplate TakeElement(Ground ground, ElementType element){
            switch (element){
                case ElementType.Electric:
                case ElementType.Physic:
                case ElementType.Real:
                    return null;
                
                
                case ElementType.Wind:
                    ground.SetType(GroundType.Null);
                    return null;
                
                case ElementType.Fire:
                    ground.SetType(GroundType.Fire);
                    return null;
                
                case ElementType.Water:
                    ground.SetType(GroundType.Water);
                    return null;
                
                case ElementType.Earth:
                    ground.SetType(GroundType.Oil);
                    return null;
                
                case ElementType.Poison:
                    ground.SetType(GroundType.Poison);
                    return null;                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(element), element, null);
            }
        }

        public IEffect OnTileObjectEnter(Ground ground, ITileObject tileObject){
            if (tileObject is not IBuffHolder holder) return null; 
            return new ChangeBuffEffect<BlindBuff>(holder, 1);
        }

        public IEffect OnTurnEnd(Ground ground, ITileObject tileObject){
            ground.SetType(GroundType.Null);
            return null;
        }

        public IEffect OnTileObjectExit(Ground ground, ITileObject tileObject){
            if (tileObject is not IBuffHolder holder) return null;
            var layer = holder.GetBuffOfType<BlindBuff>().Layer;
            if (layer <= 0) return null; 
            return new ChangeBuffEffect<BlindBuff>(holder, -layer);
        }
    }
}