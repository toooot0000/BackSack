using MVC;
using UnityEngine;

namespace Components.TileObjects{
    public interface ITileObjectView : IView{
        void MoveToPosition(Vector3 worldPosition);
        void BumpToUnsteppable(Vector2Int direction);
        void SetPosition(Vector3 worldPosition);
    }
}