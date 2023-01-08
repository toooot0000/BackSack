using System.Collections.Generic;
using Components.Buffs;
using Components.Damages;
using Components.TileObjects.ForceMovable;
using MVC;
using UnityEngine;
using Utility.Loader;

namespace Components.Enemies{
    public class Enemy: Model, IForceMovableModel, IDamageableModel{
        public Vector2Int CurrentStagePosition{ get; set; }
        public Vector2Int Size{ set; get; } = Vector2Int.one;
        public int Weight{ get; set; }
        public int HealthPoint{ get; set; }
        public int ShieldPoint{ get; set; }
        public int DefendPoint{ get; set; }
        public Dictionary<ElementType, int> Resistances{ get; set; }
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
    }
}