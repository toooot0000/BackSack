using Components.Stages;
using MVC;

namespace Components.TreasureBox{
    public class TreasureBoxController: Controller, IView{
        public StageController stageController;

        protected override void AfterSetModel(){
            transform.position = stageController.StagePositionToWorldPosition(GetModel().CurrentStagePosition);
        }

        public TreasureBox GetModel() => Model as TreasureBox;
    }
}