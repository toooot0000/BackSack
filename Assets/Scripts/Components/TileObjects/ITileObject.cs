using Components.Effects;
using MVC;
using UnityEngine;

namespace Components.TileObjects{
    public interface ITileObject : IController, IEffectConsumer{
        bool Move(Vector2Int direction);
        bool CanMoveToPosition(Vector2Int stagePosition);
        void SetPosition(Vector2Int stagePosition);
    }
}