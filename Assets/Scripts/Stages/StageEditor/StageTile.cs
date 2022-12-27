using System.IO;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace Stages.StageEditor{
    public class StageTile: Tile{
        public const string FloorTilePrefix = "tile-";
        public const string GroundEffectTilePrefix = "ground-";
        public const string TileObjectTilePrefix = "obj-";
        public string tileName = "";
        public string @params = "";

        public void CloneFrom(Tile t){
            sprite = t.sprite;
            color = t.color;
            transform = t.transform;
            gameObject = t.gameObject;
            flags = t.flags;
            colliderType = t.colliderType;
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/StageTile")]
        public static void CreateStageTile(){
            var path = EditorUtility.SaveFilePanelInProject("Save Stage Tile", "New Stage Tile", "Asset", "Save Road Tile", "assets");
            if (path == "") return;
            var instance = CreateInstance<StageTile>();
            var fileName = Path.GetFileName(path);
            var ext = Path.GetExtension(path);
            instance.tileName = fileName.Substring(0, fileName.Length - ext.Length);
            AssetDatabase.CreateAsset(instance, path);
        }
        
        [MenuItem("Tools/Convert Tile")]
        public static void ConvertTileToStageTile(){
            var selected = Selection.objects;
            foreach (var obj in selected){
                if (obj is not Tile t) continue;
                var path = AssetDatabase.GetAssetPath(obj);
                var name = t.name;
                var inst = CreateInstance<StageTile>();
                inst.CloneFrom(t);
                inst.tileName = name;
                AssetDatabase.CreateAsset(inst, path);
            }
        }
#endif
    }
}