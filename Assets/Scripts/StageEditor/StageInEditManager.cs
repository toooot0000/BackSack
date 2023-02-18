using System.Collections.Generic;
using Components.Stages.Templates;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StageEditor{
    public class StageInEditManager: MonoBehaviour{
        public string versionNumber = "1.0.0";
        public string stageName = "stage-0";
        public Tilemap floorMap;
        public Converter converter;
        public StageHardness hardness = StageHardness.Normal;

        [HideInInspector] 
        public string loadName = ""; 

        public IEnumerable<Tilemap> AllMaps => converter.AllMaps;

        public void ReadFromTemplate(StageTemplate template){
            hardness = template.Meta.hardness;
            stageName = template.Meta.name;
            versionNumber = template.Meta.version;
            converter.ReadFromTemplate(template);
        }

        public StageTemplate WriteToTemplate(){
            var ret = converter.CreateTemplate(this);
            converter.WriteToTemplate(ret);
            return ret;
        }
    }
}