using System;
using Entities.Stages;
using Models.Buffs;
using Models.EffectInfo;
using Models.TileObjects;
using MVC;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace Entities.Players{

    
    public class PlayerController: Controller<Player, PlayerView>, IAfterSetMode{
        public StageController stageController;

        public void AfterSetModel(){
            Model.CurrentStagePosition = stageController.GridPositionToStagePosition(Vector2Int.zero);
            view.SetPosition(stageController.GridPositionToWorldPosition(Vector2Int.zero));
        }
        
        private void Update(){
        }
        
        
        public void Move(Vector2Int direction){
            var dest = Model.CurrentStagePosition + direction;
            if (!stageController.IsPositionSteppable(dest)){
                view.BumpToUnsteppable(direction);
                return;
            }
            Model.CurrentStagePosition = dest;
            view.MoveToPosition(stageController.StagePositionToWorldPosition(dest));
        }


        public void Jump(){
            view.Jump();
        }
    }
}