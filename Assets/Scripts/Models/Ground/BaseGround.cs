using System.Collections.Generic;
using System.ComponentModel;
using Models.TileObjects;
using MVC;
using Utility;

namespace Models.Ground{

    public enum GroundType{
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
    
    public class BaseGround: Model{
        public string Name;
        public int LastTurnNum = 0;
        public ITileObject TileObject = null;

        private GroundType _type;
        public GroundType Type{
            set{
                _type = value;
                SetEffect(this);
            }
            get => _type;
        }

        private IGround _effect;
        public static BaseGround MakeGroundEffect(string name){
            return new BaseGround(){
                Name = name,
                Type = EnumUtility.GetValue<GroundType>(name)
            };
        }

        private static Dictionary<GroundType, IGround> _effects = null;
        private static Dictionary<GroundType, IGround> Effects{
            get{
                _effects ??= new Dictionary<GroundType, IGround>(){
                    { GroundType.Null , null},
                    { GroundType.Fire, new Fire() },
                    { GroundType.Explosion , new Explosion()},
                    { GroundType.Grass , new Grass()},
                    { GroundType.Ice , new Ice()},
                    { GroundType.Oil , new Oil()},
                    { GroundType.Poison , new Poison()},
                    { GroundType.Steam , new Steam()},
                    { GroundType.Water , new Water()}
                };
                return _effects;
            }
        }

        private static void SetEffect(BaseGround effect){
            effect._effect =  Effects[effect.Type];
        }

        public void TakeElement(ElementType element, int lastTurn){
            if (_effect == null) return;
            Type = _effect.TakeElement(element);
            LastTurnNum = Type == GroundType.Explosion ? 1 : lastTurn; // Explosion only last one turn
        }

        public void OnTurnEnd(){
            LastTurnNum--;
            if (LastTurnNum == 0){
                Type = GroundType.Null;
            }
            if(_effect is IGroundOnTurnEnd end) end.OnTurnEnd(this, TileObject);
        }

        public void OnTileObjectEnter(ITileObject tileObject){
            TileObject = tileObject;
            if(_effect is IGroundOnTileObjectEnter enter) enter.OnTileObjectEnter(this, tileObject);
        }

        public void OnTileObjectExit(ITileObject tileObject){
            TileObject = null;
            if(_effect is IGroundOnTileObjectExit exit) exit.OnTileObjectExit(this, tileObject);
        }
    }

    public interface IGround{
        public GroundType TakeElement(ElementType element);
    }

    public interface IGroundOnTileObjectEnter: IGround{
        public void OnTileObjectEnter(BaseGround ground, ITileObject tileObject);
    }

    public interface IGroundOnTileObjectExit: IGround{
        public void OnTileObjectExit(BaseGround ground, ITileObject tileObject);
    }

    public interface IGroundOnTurnEnd : IGround{
        public void OnTurnEnd(BaseGround ground, ITileObject tileObject);
    }
}