using StageEditor.MapConverters;
using StageEditor.MapConverters.Randoms;
using UnityEditor;
using UnityEngine;

namespace StageEditor.Editor{
    [CustomEditor(typeof(RandomObjectMapConverter))]
    public class RandomObjectMapConverterEditor: UnityEditor.Editor{
        public override void OnInspectorGUI(){
            base.OnInspectorGUI();
            var converter = (target as RandomObjectMapConverter)!;
            if (GUILayout.Button("Create Containers")){
                converter.CreateAllContainers();
            }

            if (GUILayout.Button("Clear Containers")){
                converter.ClearAllContainers();
            }

            if (GUILayout.Button("Clear Tiles")){
                converter.ClearAllTiles();
            }

            if (GUILayout.Button("Relink")){
                converter.RelinkContainers();
            }
        }
    }
}