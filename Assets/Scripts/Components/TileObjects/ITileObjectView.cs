using MVC;
using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects{
    public interface ITileObjectView: IView{
        void SetPosition(Vector3 worldPosition);
        void Destroy();
    }
}