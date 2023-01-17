using Components.Effects;
using Components.Stages;
using MVC;
using UnityEngine;

namespace Components.TileObjects{
    public interface ITileObject : IController, ICanConsume<MultiEffect>{
        void SetStagePosition(Vector2Int stagePosition);
        Vector2Int GetStagePosition();
        Vector3 GetWorldPosition();
        Stage GetStage();
    }
}