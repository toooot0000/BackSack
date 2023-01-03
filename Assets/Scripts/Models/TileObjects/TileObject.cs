using Models.EffectInfo;
using MVC;
using UnityEngine;

namespace Models.TileObjects{
    public interface ITileObject: IModel{
        Vector2Int CurrentStagePosition{ set; get; }
        int Weight{ set; get; }
    }

    public interface IMovableTileObject : ITileObject{
        
    }

    public interface IMoveTileObjectEffect : IEffect{
        IMovableTileObject Target{ get; }
        Vector2Int Displacement{ get; }
    }
}