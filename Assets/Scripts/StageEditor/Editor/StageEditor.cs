using System.IO;
using Components.Stages;
using Components.Stages.Templates;
using MVC;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace StageEditor.Editor{
    [CustomEditor(typeof(StageInEditManager))]
    public class StageEditor: UnityEditor.Editor{
        public string StageSavingPath => $"Assets/Resources/{stageResPath}";
        public string stageResPath = "Stages/";
        public override void OnInspectorGUI(){
            
            stageResPath = EditorGUILayout.TextField("Resources path:", stageResPath);
            EditorGUILayout.Separator();

            base.OnInspectorGUI();
            var stageInEdit = (target as StageInEditManager)!;
            if (GUILayout.Button("Export")){
                ExportTemplate(stageInEdit.WriteToTemplate());
                Debug.Log("Export complete!");
            }
            EditorGUILayout.Separator();
            
            stageInEdit.loadName = EditorGUILayout.TextField("Name of the file to load:", stageInEdit.loadName);
            if (GUILayout.Button("Load")){
                var stage = LoadTemplate(stageInEdit.loadName);
                if (stage == null) return;
                Debug.Log("Load complete!");
                stageInEdit.ReadFromTemplate(stage);
            }
            
            if (GUILayout.Button("Select a stage template to load")){
                var path = EditorUtility.OpenFilePanelWithFilters("Select a stage template", StageSavingPath, new []{ "JSON", "json"});
                if (!string.IsNullOrEmpty(path)){
                    var fullName = Path.GetFileName(path);
                    var ext = Path.GetExtension(path);
                    var stage = LoadTemplate(fullName[..^ext.Length]);
                    if (stage != null){
                        stageInEdit.ReadFromTemplate(stage);
                    }
                } 
            }

            if (GUILayout.Button("Clear All")){
                foreach (var map in stageInEdit.converter.mapConverters){
                    map.Clear();
                }
            }

            if (GUILayout.Button("Reload dictionaries")){
                foreach (var map in stageInEdit.converter.mapConverters){
                    map.Reload();
                }
            }
        }

        private StageTemplate LoadTemplate(string fileName){
            if (!fileName.StartsWith(stageResPath)) fileName = $"{stageResPath}{fileName}";
            Debug.Log($"Loading stage file: {fileName}");
            var ret = Resources.Load<StageTemplate>(fileName);
            if (ret != null) return ret;
            Debug.LogError($"Loading stage FAILED! Can't find the stage file with path of {fileName}");
            return null;
        }
        
        
        private void ExportTemplate(StageTemplate template){
            AssetDatabase.SaveAssets();
            var filePath = $"{StageSavingPath}{template.meta.name}.asset";
            Debug.Log($"Export stage data to file: {filePath}");
            if (!Directory.Exists(StageSavingPath)) Directory.CreateDirectory(StageSavingPath);
            AssetDatabase.CreateAsset(template, filePath);
            AssetDatabase.Refresh();
        }
    }
}