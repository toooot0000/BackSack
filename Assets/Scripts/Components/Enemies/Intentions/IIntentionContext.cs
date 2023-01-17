using Components.Players;
using Components.Stages;

namespace Components.Enemies.Intentions{

    public interface IIntentionContext{
        public Stage Stage{ get; }
        public Player Player{ get; }
    }

    public class IntentionContext: IIntentionContext{
        public IntentionContext(Stage stage, Player player){
            Stage = stage;
            Player = player;
        }
        
        public Stage Stage{ get; }
        public Player Player{ get; }
    }
}