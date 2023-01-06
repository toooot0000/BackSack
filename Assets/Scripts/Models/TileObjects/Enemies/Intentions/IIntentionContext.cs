using Models.Stages;

namespace Models.TileObjects.Enemies.Intentions{

    public interface IIntentionContext{
        public Stage Stage{ get; }
        public Player Player{ get; }
    }
}