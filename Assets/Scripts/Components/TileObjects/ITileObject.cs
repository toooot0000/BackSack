using Components.Effects;
using MVC;
using UnityEngine;

namespace Components.TileObjects{
    public interface ITileObject : IController, IEffectConsumer{
        IEffect Move(Vector2Int stagePosition);
        bool CanMoveToPosition(Vector2Int stagePosition);
        void SetStagePosition(Vector2Int stagePosition);
        Vector2Int GetStagePosition();
    }
}