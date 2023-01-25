using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects.Movables{
    public interface IMovableView: ITileObjectView{
        void MoveToPosition(Vector3 worldPosition);
        void BumpToUnsteppable(Direction direction);
    }
}