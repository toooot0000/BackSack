using Components.TileObjects;
using MVC;
using UnityEngine;

namespace Components.TreasureBox{
    public class TreasureBox: Model, ITileObjectModel{
        public Vector2Int CurrentStagePosition{ get; set; }
        public int Weight{ get; set; }
        public Vector2Int Size{ set; get; } = Vector2Int.one;
    }
}