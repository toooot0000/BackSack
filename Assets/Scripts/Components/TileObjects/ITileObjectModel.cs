using MVC;
using UnityEngine;

namespace Components.TileObjects{
    public interface ITileObjectModel: IModel{
        Vector2Int CurrentStagePosition{ set; get; }
        Vector2Int Size{ get; }
    }
}