using Components.Effects;
using Components.TileObjects.Effects;
using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects.Movables{
    public interface IMovable: ITileObject, ICanConsume<IForceMovement>{
        IEffect TryMove(Direction direction);
        IEffect MoveTo(Vector2Int targetStagePosition);
        bool CanMoveToPosition(Vector2Int stagePosition);
        int Weight{ set; get; }
    }
}