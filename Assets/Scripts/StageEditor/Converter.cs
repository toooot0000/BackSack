using Components.Stages;
using UnityEngine.Tilemaps;

namespace StageEditor{
    public class Converter{

        public StageModel ToStageModel(StageInEditManager manager){
            var size = manager.floorMap.size;
            var ret = new StageModel{
                Floors = new Floor[size.x, size.y],
                Meta = new StageMeta{
                    Version = manager.versionNumber,
                    Height = size.y,
                    Width = size.x,
                    Name = manager.stageName
                }
            };
            for (var i = 0; i < size.x; i++){
                for (var j = 0; j < size.y; j++){
                    if (!manager.SetFloorTypeInModel(i, j, ret)) continue;
                    manager.SetObjectInModel(i, j, ret);
                    manager.SetGroundEffectInModel(i, j, ret);
                }
            }
            return ret;
        }
        
        public void LoadFromStageModel(StageInEditManager manager, StageModel source){

            foreach (var map in manager.AllMaps){
                map.ClearAllTiles();
            }
            
            manager.versionNumber = source.Meta.Version;
            manager.stageName = source.Meta.Name;
            for (int i = 0; i < source.Width; i++){
                for (int j = 0; j < source.Height; j++){
                    manager.SetMapFloorFromModel(i, j, source);
                    manager.SetMapObjectFromModel(i, j, source);
                    manager.SetMapGroundEffectFromModel(i, j, source);
                }
            }
        }
    }
}