using Components.Stages;
using MVC;
using UnityEngine;

namespace Components.Enemies{
    public class EnemyController: Controller{

        public StageController stageController;
        public EnemyView view;

        protected override void AfterSetModel(){
            var sprite = Resources.Load<Sprite>(GetModel().GetSpriteResourcePath());
            if (sprite != null) view.SetSprite(sprite);
            view.SetPosition(stageController.StagePositionToWorldPosition(GetModel().CurrentStagePosition));
        }
        
        public Enemy GetModel() => Model as Enemy;
    }
}