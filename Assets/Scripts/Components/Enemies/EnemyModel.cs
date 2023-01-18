using System.Collections.Generic;
using Components.Buffs;
using Components.Damages;
using Components.TileObjects.BattleObjects;
using Components.TileObjects.ForceMovables;
using MVC;
using UnityEngine;
using Utility.Loader;
using Utility.Loader.Attributes;

namespace Components.Enemies{
    [Table("enemies")]
    public class EnemyModel: SelfSetUpModel, IBattleObjectModel{
        public Vector2Int CurrentStagePosition{ get; set; }
        public Vector2Int Size{ set; get; } = Vector2Int.one;
        public int Weight{ get; set; }
        public int HealthPoint{ get; set; }
        public int ShieldPoint{ get; set; }
        public int DefendPoint{ get; set; }
        public Dictionary<ElementType, int> Resistances{ get; set; }

        [Key("sprite_path")] 
        public string SprPath;
        
        [Key("core_path")]
        public string CorePath;
        
        
        public string GetSpriteResourcePath() => "Images/Tiles/Enemies/Enemy-slime"; // TODO

        public EnemyModel(int id){
            ID = id;
        }
    }
}