using System.IO;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace StageEditor.Tiles{
    public class StageTile: Tile{
        public const string FloorTilePrefix = "tile-";
        public const string GroundEffectTilePrefix = "ground-";
        public const string TileObjectTilePrefix = "obj-";
        public string tileName = "";
        public string @params = "";

        protected void CloneFrom(Tile t){
            sprite = t.sprite;
            color = t.color;
            transform = t.transform;
            gameObject = t.gameObject;
            flags = t.flags;
            colliderType = t.colliderType;
        }

#if UNITY_EDITOR

        private static void CreateTileViaPanel<T>() where T : StageTile{
            var path = EditorUtility.SaveFilePanelInProject("Save Stage Tile", "New Stage Tile", "asset", "Save Road Tile", "assets");
            if (path == "") return;
            var instance = CreateInstance<T>();
            AssetDatabase.CreateAsset(instance, path);
        }
        
        protected static void CreateTile<T>() where T: StageTile{
            var obj = Selection.activeObject;
            var path = AssetDatabase.GetAssetPath(obj);
            if (string.IsNullOrEmpty(path)){
                CreateTileViaPanel<T>();
            } else{
                if (!AssetDatabase.IsValidFolder(path)){
                    path = Path.GetDirectoryName(path);
                }
                var filename = "new tile";
                var fileCount = 0;
                var finalPath = $"{path}/{filename}.asset";
                while(File.Exists(finalPath)){
                    fileCount++;
                    finalPath = $"{path}/{filename}{fileCount}.asset";
                }
                var inst = CreateInstance<T>();
                ProjectWindowUtil.CreateAsset(inst, finalPath);
                // AssetDatabase.CreateAsset(inst, path);
            }
            // var path = EditorUtility.SaveFilePanelInProject("Save Stage Tile", "New Stage Tile", "asset", "Save Road Tile", "assets");
            // if (path == "") return;
            // var instance = CreateInstance<T>();
            // AssetDatabase.CreateAsset(instance, path);
        }

        [MenuItem("Tools/Tiles/Convert to StageTile")]
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