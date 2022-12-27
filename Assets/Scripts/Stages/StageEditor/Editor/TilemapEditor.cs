using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Stages.StageEditor.Editor{
    [CustomEditor(typeof(Tilemap))]
    public class TilemapEditor: UnityEditor.Editor{
        
        public override void OnInspectorGUI(){
            base.OnInspectorGUI();
            if (GUILayout.Button("Clear All")){
                (target as Tilemap)?.ClearAllTiles();
            }
        }
        
    }
}