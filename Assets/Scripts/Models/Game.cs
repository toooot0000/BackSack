using System.ComponentModel;
using Models.TileObjects;
using MVC;

namespace Models{

    public enum ElementType{
        [Description("fire")]
        Fire,
        [Description("water")]
        Water,
        [Description("wind")]
        Wind,
        [Description("earth")]
        Earth,
        [Description("electric")]
        Electric,
        [Description("poison")]
        Poison,
        [Description("physic")]
        Physic,
        [Description("real")]
        Real,
    }
    
    public class Game: Model{
        private static Game _shared;
        public static Game Shared => _shared ??= new Game();
        public Player Player;
        public Level CurrentLevel = null;
        
    }
}