using System.Collections.Generic;
using Models.Buffs;
using Models.Damages;
using MVC;
using UnityEngine;

namespace Models.TileObjects{
    public class Player : Model, ITileObject, IBuffHolder, IDamageable{
        public Vector2Int CurrentStagePosition{ get; set; } = Vector2Int.zero;
        public int Weight{ get; set; }

        public int HealthPoint{ get; set; }
        public int ShieldPoint{ get; set; }
        public int DefendPoint{ get; set; }
        public Dictionary<ElementType, int> Resistances{ get; set; }
        public void TakeDamage(Damage damage){
            throw new System.NotImplementedException();
        }
        public List<Buff> Buffs{ get; set; }
        
    }
}