using System.ComponentModel;
using Components.Levels;
using Components.Players;
using MVC;

namespace Components{

    public enum ElementType{
        [Description("fire")]
        Fire = 0,
        [Description("water")]
        Water = 1,
        [Description("wind")]
        Wind = 2,
        [Description("earth")]
        Earth = 3,
        [Description("electric")]
        Electric = 4,
        [Description("poison")]
        Poison = 5,
        [Description("physic")]
        Physic = 6,
        [Description("real")]
        Real = 7,
        Size = 8
    }
    
    public enum GameState{
        Exploring,
        Pausing, // => When UI Opened
    }
    
    public class Game: Model{
        private static Game _shared;
        public static Game Shared => _shared ??= new Game();
        public GameState State = GameState.Exploring;
       

    }
}