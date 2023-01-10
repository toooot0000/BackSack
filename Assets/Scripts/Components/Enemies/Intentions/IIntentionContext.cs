using Components.Players;
using Components.Stages;

namespace Components.Enemies.Intentions{

    public interface IIntentionContext{
        public StageModel StageModel{ get; }
        public PlayerModel PlayerModel{ get; }
        public EnemyModel EnemyModel{ get; }
    }

    public class IntentionContext: IIntentionContext{
        public StageModel StageModel{ set; get; }
        public PlayerModel PlayerModel{ set; get; }
        public EnemyModel EnemyModel{ set; get; }
    }
}