using System.Collections.Generic;
using MVC;

namespace Models{
    public class Level: Model{
        public Stage.Stage CurrentStage;
        public List<Stage.Stage> Stages;
    }
}     