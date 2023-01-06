using Entities.Stages;
using Models.TileObjects;
using MVC;
using UnityEngine;

namespace Entities.TileObjects{
    
    public abstract class TileObjectsController: Controller<ITileObject, IViewWithType<ITileObject>>{
        public StageController stageController;
        public abstract void Move(Vector2Int displacement);
    }
}