using System.Collections.Generic;
using Components.Stages;
using MVC;

namespace Components.Levels{
    public class Level: Model{
        public Stage CurrentStage;
        public List<Stage> Stages;
    }
}     