using System;
using Entities.Stages;
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
            if (Input.GetKeyUp(KeyCode.UpArrow)){
                Move(Vector2Int.up);
            } else if (Input.GetKeyUp((KeyCode.LeftArrow))){
                Move(Vector2Int.left);
            } else if (Input.GetKeyUp(KeyCode.DownArrow)){
                Move(Vector2Int.down);
            } else if (Input.GetKeyUp(KeyCode.RightArrow)){
                Move(Vector2Int.right);
            }
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
        
    }
}