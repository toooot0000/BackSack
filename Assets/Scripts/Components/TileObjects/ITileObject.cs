using Components.Effects;
using Components.Stages;
using MVC;
using UnityEngine;

namespace Components.TileObjects{
    public interface ITileObject : IController, ICanConsume<MultiEffect>{
        Vector2Int CurrentStagePosition{ set; get; }
        Vector3 Position{ set; get; }
        Vector2Int Size{ get; }
    }
}