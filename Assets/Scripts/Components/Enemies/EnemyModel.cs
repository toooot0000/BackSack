using UnityEngine;
using Utility.Loader.Attributes;

namespace Components.Enemies{
    [Table("enemies")]
    public class EnemyModel: SelfSetUpModel{
        public Vector2Int CurrentStagePosition;
        public Vector2Int Size = Vector2Int.one;
        public int Weight;
        public int HealthLimit;
        public int HealthPoint;
        public int ShieldPoint;
        public int DefendPoint;

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