using Entities.Stages;
using Models.TileObjects;
using Models.TileObjects.Enemies;
using MVC;
using UnityEngine;

namespace Entities.Enemies{
    public class EnemyController: Controller<Enemy, EnemyView>, IAfterSetMode{

        public StageController stageController;

        public void AfterSetModel(){
            var sprite = Resources.Load<Sprite>(Model.GetSpriteResourcePath());
            if (sprite != null) view.SetSprite(sprite);
            view.SetPosition(stageController.StagePositionToWorldPosition(Model.CurrentStagePosition));
        }
        
        
    }
}