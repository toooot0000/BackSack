using System;
using Components.Effects;
using Components.Grounds.Reducer;

namespace Components.Grounds.Instances{
    public class Oil : IReducer{

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
    }
}