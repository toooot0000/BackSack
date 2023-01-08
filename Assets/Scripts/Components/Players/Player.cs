using System.Collections.Generic;
using Components.Buffs;
using Components.Damages;
using Components.TileObjects;
using Components.TileObjects.ForceMovable;
using MVC;
using UnityEngine;

namespace Components.Players{
    public class Player : Model, IDamageableModel, IBuffHolderModel, IForceMovableModel{
        public Vector2Int CurrentStagePosition{ get; set; }
        public Vector2Int Size{ get; set; }
        public List<Buff> Buffs{ get; set; }

        public int HealthPoint{ get; set; }
        public int ShieldPoint{ get; set; }
        public int DefendPoint{ get; set; }
        public Dictionary<ElementType, int> Resistances{ get; set; }

        public int Weight{ get; set; }
    }
}