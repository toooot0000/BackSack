using Components.Effects;
using Components.Stages;
using MVC;
using UnityEngine;

namespace Components.TileObjects{
    public interface ITileObject : IController, IEffectConsumer, ICanConsume<MultiEffect>{
        IEffect Move(Vector2Int stagePosition);
        bool CanMoveToPosition(Vector2Int stagePosition);
        void SetStagePosition(Vector2Int stagePosition);
        Vector2Int GetStagePosition();
        Vector3 GetWorldPosition();
        Stage GetStage();
    }
}