using System.Collections.Generic;
using Models.Buffs;
using Models.Damages;
using Models.TileObjects.Enemies.Intentions;
using MVC;
using UnityEngine;
using Utility.Loader;

namespace Models.TileObjects.Enemies{
    public class Enemy: Model, ITileObject, IBuffHolder, IDamageable{
        public Vector2Int CurrentStagePosition{ get; set; }
        public int Weight{ get; set; }
        public int HealthPoint{ get; set; }
        public int ShieldPoint{ get; set; }
        public int DefendPoint{ get; set; }
        public Dictionary<ElementType, int> Resistances{ get; set; }
        public void TakeDamage(Damage damage){
            throw new System.NotImplementedException();
        }

        public List<Buff> Buffs{ get; set; }

        public string GetSpriteResourcePath() => "Images/Tiles/Enemies/Enemy-slime"; // TODO

        public static Enemy MakeEnemy(int id){
            var ret = new Enemy(){ID = id};
            if (!ret.StartFieldSetting("enemies")){
                Debug.LogError($"Invalid Id {id}!");
                return null;
            };
            ret.Name = ret.GetField<string>("name");
            ret.Desc = ret.GetField<string>("desc");
            ret.EndFieldSetting();
            return ret;
        }

        public IEnemyIntention GetIntention(IIntentionContext context){
            return null;
        }
    }
}