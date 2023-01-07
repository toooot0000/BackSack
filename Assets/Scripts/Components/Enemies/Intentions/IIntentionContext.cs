using Components.Players;
using Components.Stages;

namespace Components.Enemies.Intentions{

    public interface IIntentionContext{
        public Stage Stage{ get; }
        public Player Player{ get; }
        public Enemy Enemy{ get; }
    }

    public class IntentionContext: IIntentionContext{
        public Stage Stage{ set; get; }
        public Player Player{ set; get; }
        public Enemy Enemy{ set; get; }
    }
}