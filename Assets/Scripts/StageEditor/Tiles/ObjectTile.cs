using System.IO;
using Components.TileObjects;
using UnityEditor;
using UnityEngine.Tilemaps;
using Utility;

namespace StageEditor.Tiles{
    public class ObjectTile: StageTile{
        public TileObjectType type = TileObjectType.Null;
        public int id = 0;
        
#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tile/ObjectTile")]
        public static void CreateObjectTile(){
            CreateTile<ObjectTile>();
        }


        [MenuItem("Tools/Tiles/Convert To ObjectTile")]
        public static void ConvertTileToObjectTile(){
            var selected = Selection.objects;
            foreach (var obj in selected){
                if (obj is not Tile t) continue;
                var path = AssetDatabase.GetAssetPath(obj);
                var name = t.name;
                var inst = CreateInstance<ObjectTile>();
                inst.CloneFrom(t);
                inst.tileName = name;
                inst.id = IntUtility.ParseString(inst.@params);
                if (name.ToLower().Contains("enemy")){
                    inst.type = TileObjectType.Enemy;
                } else if (name.ToLower().Contains("treasure")){
                    inst.type = TileObjectType.Treasure;
                }
                AssetDatabase.CreateAsset(inst, path);
            }
        }
#endif
    }
}