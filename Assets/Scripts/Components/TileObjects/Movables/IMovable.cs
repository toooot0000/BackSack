using Components.Effects;
using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects.Movables{
    public interface IMovable: ITileObject{
        IEffect Move(Direction direction);
        bool CanMoveToPosition(Vector2Int stagePosition);
    }
}