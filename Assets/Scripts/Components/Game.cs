using System.ComponentModel;
using Components.Levels;
using Components.Players;
using MVC;

namespace Components{

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