using System.Collections.Generic;
using Models.EffectInfo;
using Models.Ground.Instances;
using Models.Ground.Reducer;
using Models.Ground.Triggers;
using Models.TileObjects;
using MVC;
using Utility;

namespace Models.Ground{
    public class Ground: Model{
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

        private IReducer _effect;
        public static Ground MakeGroundEffect(string name){
            return new Ground(){
                Name = name,
                Type = EnumUtility.GetValue<GroundType>(name)
            };
        }

        private static Dictionary<GroundType, IReducer> _effects = null;
        private static Dictionary<GroundType, IReducer> Effects{
            get{
                _effects ??= new Dictionary<GroundType, IReducer>(){
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

        private static void SetEffect(Ground effect){
            effect._effect =  Effects[effect.Type];
        }

        public void TakeElement(ElementType element, int lastTurn){
            if (_effect == null) return;
            Type = _effect.TakeElement(element);
            LastTurnNum = Type == GroundType.Explosion ? 1 : lastTurn; // Explosion only last one turn
        }

        public IEffect OnTurnEnd(){
            LastTurnNum--;
            if (LastTurnNum == 0){
                Type = GroundType.Null;
            }
            if(_effect is IOnTurnEnd end) return end.OnTurnEnd(this, TileObject);
            return null;
        }

        public IEffect OnTileObjectEnter(ITileObject tileObject){
            TileObject = tileObject;
            if(_effect is IOnTileObjectEnter enter) return enter.OnTileObjectEnter(this, tileObject);
            return null;
        }

        public IEffect OnTileObjectExit(ITileObject tileObject){
            TileObject = null;
            if (_effect is IOnTileObjectExit exit) return exit.OnTileObjectExit(this, tileObject);
            return null;
        }
    }
}