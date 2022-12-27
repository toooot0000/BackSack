using Models.TileObjects;
using MVC;
using Stages;

namespace Players{
    
    public class PlayerManager: Manager<Player, PlayerView>, IAfterSetMode{
        public StageManager stageManager;
        public void AfterSetModel(){
            transform.position =
                stageManager.GridPositionToWorldPosition(
                    stageManager.StagePositionToGridPosition(model.CurrentStagePosition));
        }
    
    }
}