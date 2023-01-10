using System.Collections.Generic;
using Components.Stages;
using MVC;

namespace Components.Levels{
    public class Level: Model{
        public StageModel CurrentStageModel;
        public List<StageModel> Stages;
    }
}     