using MVC;
using UnityEngine;

namespace Models.TileObjects{
    public class TreasureBox: Model, ITileObject{
        public Vector2Int CurrentStagePosition{ get; set; }
        public int Weight{ get; set; }
    }
}