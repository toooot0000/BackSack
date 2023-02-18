using System;
using System.Collections.Generic;
using System.ComponentModel;
using Components.Grounds;
using Components.Stages.Floors;
using Components.TileObjects;
using MVC;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Components.Stages.Templates{

    public enum StageHardness{
        Normal,
        Hard,
    }
    
    [Serializable]
    public struct PositionValuePair<T>{
        public Vector2Int position;
        public T value;
    }
    
    [Serializable]
    public struct TileObjectInfo{
        public TileObjectType type;
        public int id;
    }

    public class StageTemplate: ScriptableObject{
        
        public StageMeta Meta;
        public RandomObjectGroup[] randomGroups;
        
        public PositionValuePair<FloorType>[] floors;
        public PositionValuePair<TileObjectInfo>[] tileObjects;
        public PositionValuePair<GroundType>[] grounds;

        public StageModel ToModel(){
            
            var ret = new StageModel(Meta);
            foreach (var pair in floors){
                ret.Floors[pair.position.x, pair.position.y].Type = pair.value;
            }

            foreach (var pair in tileObjects){
                ret.Floors[pair.position.x, pair.position.y].TileObjectType = pair.value.type;
                ret.Floors[pair.position.x, pair.position.y].TileObjectId = pair.value.id;
            }

            foreach (var pair in grounds){
                ret.Floors[pair.position.x, pair.position.y].GroundType = pair.value;
            }

            return ret;
        }
    }
}