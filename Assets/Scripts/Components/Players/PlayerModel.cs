using System.Collections.Generic;
using Components.TileObjects.BattleObjects;
using MVC;
using UnityEngine;

namespace Components.Players{
    public class PlayerModel : Model, IBattleObjectModel{
        public Vector2Int CurrentStagePosition{ get; set; }
        public Vector2Int Size{ get; set; }

        public int HealthPoint{ get; set; }
        public int ShieldPoint{ get; set; }
        public int DefendPoint{ get; set; }
        public Dictionary<ElementType, int> Resistances{ get; set; }
        public int Weight{ get; set; }
    }
}