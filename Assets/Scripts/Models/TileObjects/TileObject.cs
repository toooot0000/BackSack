using MVC;
using UnityEngine;

namespace Models.TileObjects{
    public abstract class TileObject: Model{
        public Vector2Int CurrentPosition = Vector2Int.zero;
    }

    public interface ITileObject{
        public Vector2Int CurrentStagePosition{ set; get; }
    }
}