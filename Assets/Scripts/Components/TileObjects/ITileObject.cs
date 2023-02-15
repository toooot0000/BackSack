using System;
using Components.Effects;
using Components.Stages;
using MVC;
using UnityEngine;

namespace Components.TileObjects{
    public interface ITileObject : IController, ICanConsume<MultiEffect>{
        Vector2Int CurrentStagePosition{ get; }
        Vector3 Position{ get; }
        Vector2Int Size{ get; }
        void SetStagePosition(Vector2Int stagePosition);
        bool CanSetPositionForGivenFloorType(FloorType floor);
        bool IsDestroyed{ get; }
        void Destroy();
    }
}