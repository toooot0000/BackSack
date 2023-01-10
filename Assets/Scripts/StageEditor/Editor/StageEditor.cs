using System.IO;
using Components.Stages;
using MVC;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace StageEditor.Editor{
    [CustomEditor(typeof(StageInEditManager))]
    public class StageEditor: UnityEditor.Editor{
        public string StageSavingPath => $"{Application.dataPath}/Resources/{stageResPath}";
        public string stageResPath = "Stages/";
        public override void OnInspectorGUI(){
            
            stageResPath = EditorGUILayout.TextField("Resources path:", stageResPath);
            EditorGUILayout.Separator();

            base.OnInspectorGUI();
            var stageInEdit = (target as StageInEditManager)!;
            if (GUILayout.Button("Export")){
                Export(stageInEdit.ToStage());
                Debug.Log("Export complete!");
            }
            EditorGUILayout.Separator();

            stageInEdit.loadName = EditorGUILayout.TextField("Name of the file to load:", stageInEdit.loadName);
            if (GUILayout.Button("Load")){
                var stage = Load(stageInEdit.loadName);
                if (stage == null) return;
                Debug.Log("Load complete!");
                stageInEdit.FromStage(stage);
            }

            if (GUILayout.Button("Select a stage to load")){
                var path = EditorUtility.OpenFilePanelWithFilters("Select a stage", StageSavingPath, new []{ "JSON", "json"});
                if (!string.IsNullOrEmpty(path)){
                    var fullName = Path.GetFileName(path);
                    var ext = Path.GetExtension(path);
                    var stage = Load(fullName[..^ext.Length]);
                    if (stage != null){
                        stageInEdit.FromStage(stage);
                    }
                } 
            }

            if (GUILayout.Button("Clear All")){
                foreach (var map in stageInEdit.AllMaps){
                    map.ClearAllTiles();
                }
            }
        }

        private StageModel Load(string fileName){
            if (!fileName.StartsWith(stageResPath)) fileName = $"{stageResPath}{fileName}";
            Debug.Log($"Loading stage file: {fileName}");
            var textAsset = Resources.Load<TextAsset>(fileName);
            if (textAsset == null){
                Debug.LogError($"Loading stage FAILED! Can't find the stage file with path of {fileName}");
                return null;
            }
            return Model.FromJsonString<StageModel>(textAsset.text);
        }

        private void Export(StageModel stageModel){
            AssetDatabase.SaveAssets();
            var filePath = $"{StageSavingPath}{stageModel.Meta.Name}.json";
            Debug.Log($"Export stage data to file: {filePath}");
            if (!Directory.Exists(StageSavingPath)) Directory.CreateDirectory(StageSavingPath);
            File.WriteAllText(filePath, stageModel.ToJson(Formatting.Indented));
            AssetDatabase.Refresh();
        }
    }
}