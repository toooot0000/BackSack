using System;
using System.Collections.Generic;
using System.ComponentModel;
using Models.TileObjects;
using MVC;
using UnityEngine;
using Utility;
using Utility.Extensions;

namespace Models.GroundEffects{

    public enum GroundEffectType{
        [Description("null")]
        Null,
        [Description("fire")]
        Fire,
        [Description("water")]
        Water,
        [Description("poison")]
        Poison,
        [Description("oil")]
        Oil,
        [Description("ice")]
        Ice,
        [Description("grass")]
        Grass,
        [Description("explosion")]
        Explosion,
        [Description("steam")]
        Steam
    }
    
    public class BaseGroundEffect: Model{
        public string Name;
        public int LastTurnNum = 0;
        public ITileObject TileObject = null;

        private GroundEffectType _type;
        public GroundEffectType EffectType{
            set{
                _type = value;
                SetEffect(this);
            }
            get => _type;
        }

        private IGroundEffect _effect;
        public static BaseGroundEffect MakeGroundEffect(string name){
            return new BaseGroundEffect(){
                Name = name,
                EffectType = EnumUtility.GetValue<GroundEffectType>(name)
            };
        }

        private static Dictionary<GroundEffectType, IGroundEffect> _effects = null;
        private static Dictionary<GroundEffectType, IGroundEffect> Effects{
            get{
                _effects ??= new Dictionary<GroundEffectType, IGroundEffect>(){
                    { GroundEffectType.Null , null},
                    { GroundEffectType.Fire, new FireEffect() },
                    { GroundEffectType.Explosion , new ExplosionEffect()},
                    { GroundEffectType.Grass , new GrassEffect()},
                    { GroundEffectType.Ice , new IceEffect()},
                    { GroundEffectType.Oil , new OilEffect()},
                    { GroundEffectType.Poison , new PoisonEffect()},
                    { GroundEffectType.Steam , new SteamEffect()},
                    { GroundEffectType.Water , new WaterEffect()}

                };
                return _effects;
            }
        }

        private static void SetEffect(BaseGroundEffect effect){
            effect._effect =  Effects[effect.EffectType];
        }

        public void TakeEffect(ElementType element, int lastTurn){
            if (_effect == null) return;
            EffectType = _effect.TakeElement(element);
            LastTurnNum = EffectType == GroundEffectType.Explosion ? 1 : lastTurn; // Explosion only last one turn
        }

        public void OnTurnEnd(){
            LastTurnNum--;
            if (LastTurnNum == 0){
                EffectType = GroundEffectType.Null;
            }
            if(_effect is IGroundEffectOnTurnEnd end) end.OnTurnEnd(this, TileObject);
        }

        public void OnTileObjectEnter(ITileObject tileObject){
            TileObject = tileObject;
            if(_effect is IGroundEffectOnTileObjectEnter enter) enter.OnTileObjectEnter(this, tileObject);
        }

        public void OnTileObjectExit(ITileObject tileObject){
            TileObject = null;
            if(_effect is IGroundEffectOnTileObjectExit exit) exit.OnTileObjectExit(this, tileObject);
        }
    }

    public interface IGroundEffect{
        public GroundEffectType TakeElement(ElementType element);
    }

    public interface IGroundEffectOnTileObjectEnter: IGroundEffect{
        public void OnTileObjectEnter(BaseGroundEffect groundEffect, ITileObject tileObject);
    }

    public interface IGroundEffectOnTileObjectExit: IGroundEffect{
        public void OnTileObjectExit(BaseGroundEffect groundEffect, ITileObject tileObject);
    }

    public interface IGroundEffectOnTurnEnd : IGroundEffect{
        public void OnTurnEnd(BaseGroundEffect groundEffect, ITileObject tileObject);
    }
}