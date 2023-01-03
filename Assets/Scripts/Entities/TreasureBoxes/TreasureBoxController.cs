using Entities.Stages;
using Models.TileObjects;
using MVC;

namespace Entities.TreasureBoxes{
    public class TreasureBoxController: Controller<TreasureBox, IViewWithType<TreasureBox>>, IViewWithType<TreasureBox>, IAfterSetMode{
        public StageController stageController;
        private void Start(){
            view = this;
        }

        public void AfterSetModel(){
            transform.position = stageController.StagePositionToWorldPosition(Model.CurrentStagePosition);
        }
        
    }
}