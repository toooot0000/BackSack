using Components.TileObjects;
using MVC;
using UnityEngine;

namespace Components.TreasureBoxes{
    public class TreasureBoxModel: Model, ITileObjectModel{
        public Vector2Int CurrentStagePosition{ get; set; }
        public int Weight{ get; set; }
        public Vector2Int Size{ set; get; } = Vector2Int.one;
    }
}