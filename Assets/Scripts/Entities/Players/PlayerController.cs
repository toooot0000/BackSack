using Entities.Stages;
using Models.TileObjects;
using MVC;
using UnityEngine;

namespace Entities.Players{

    
    public class PlayerController: Controller<Player, PlayerView>, IAfterSetMode{
        public StageController stageController;
        public void AfterSetModel(){
            view.SetPosition(
                stageController.GridPositionToWorldPosition(
                    stageController.StagePositionToGridPosition(Model.CurrentStagePosition)
                )
            );
        }
        
        
        public void Move(Vector2Int direction){
            var dest = stageController.ClampToStageRange(Model.CurrentStagePosition + direction);
            if (dest == Model.CurrentStagePosition){
                view.BumpToWall(direction);
                return;
            }
            Model.CurrentStagePosition = dest;
            view.MoveToPosition(stageController.StagePositionToWorldPosition(dest));
        }
    }
}