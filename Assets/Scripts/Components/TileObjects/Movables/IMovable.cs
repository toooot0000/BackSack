using Components.Effects;
using UnityEngine;

namespace Components.TileObjects.Movables{
    public interface IMovable: ITileObject{
        IEffect Move(Vector2Int direction);
        bool CanMoveToPosition(Vector2Int stagePosition);
    }
}