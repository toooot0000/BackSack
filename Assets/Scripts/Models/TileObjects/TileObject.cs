using MVC;
using UnityEngine;

namespace Models.TileObjects{
    public interface ITileObject: IModel{
        Vector2Int CurrentStagePosition{ set; get; }
        int Weight{ set; get; }
        
    }
}