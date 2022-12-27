using MVC;
using UnityEngine;

namespace Models.TileObjects{
    public class Player: Model, ITileObject{
        public Vector2Int CurrentStagePosition{ get; set; } = Vector2Int.zero;
        public int Hp = 10;
        public int Energy = 3;
    }
}