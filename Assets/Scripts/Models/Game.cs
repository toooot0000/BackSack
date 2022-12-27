using Models.TileObjects;
using MVC;

namespace Models{

    public enum ElementType{
        Fire,
        Water,
        Wind,
        Earth,
        Electric,
        Soul,
        Physic,
    }
    
    public class Game: Model{
        private static Game _shared;
        public static Game Shared => _shared ??= new Game();
        public Player Player;
        public Level CurrentLevel = null;
        
    }
}