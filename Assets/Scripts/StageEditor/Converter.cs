using System.Collections.Generic;
using System.Linq;
using Components.Stages;
using Components.Stages.Floors;
using Components.Stages.Templates;
using StageEditor.MapConverters;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StageEditor{
    public class Converter: MonoBehaviour{

        public Tilemap floorMap;
        public MapConverter[] mapConverters;
        public IEnumerable<Tilemap> AllMaps => mapConverters.Select(c => c.map);

        public StageTemplate CreateTemplate(StageInEditManager manager){
            var size = manager.floorMap.size;
            var ret = ScriptableObject.CreateInstance<StageTemplate>();
            ret.Meta = new StageMeta{
                version = manager.versionNumber,
                height = size.y,
                width = size.x,
                name = manager.stageName
            };
            return ret;
        }
        
        public void ReadFromTemplate(StageTemplate template){
            foreach (var converter in mapConverters){
                converter.ReadFromTemplate(template);
            }
        }

        public void WriteToTemplate(StageTemplate template){
            foreach (var converter in mapConverters){
                converter.WriteToTemplate(template);
            }
        }
    }
}