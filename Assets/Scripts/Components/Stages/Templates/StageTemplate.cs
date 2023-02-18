using System;
using System.Collections.Generic;
using System.Linq;
using Components.Grounds;
using Components.Stages.Floors;
using Components.TileObjects;
using UnityEngine;
using Utility.Extensions;

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
        
        public StageMeta meta;

        public PositionValuePair<Direction>[] connectedDirection;
        
        public RandomObjectGroup[] randomGroups;
        
        public PositionValuePair<FloorType>[] floors;
        public PositionValuePair<TileObjectInfo>[] tileObjects;
        public PositionValuePair<GroundType>[] grounds;

        private HashSet<Direction> _directions;
        public HashSet<Direction> AvailableDirections{
            get{
                _directions ??= connectedDirection.Select(p => p.value).ToHashSet();
                return _directions;
            }
        }

        public StageModel ToModel(){
            
            var ret = new StageModel(meta);
            foreach (var pair in floors){
                ret.GetFloor(ret.GetStagePosition(pair.position)).Type = pair.value;
            }

            foreach (var pair in tileObjects){
                ret.GetFloor(ret.GetStagePosition(pair.position)).TileObjectType = pair.value.type;
                ret.GetFloor(ret.GetStagePosition(pair.position)).TileObjectId = pair.value.id;
            }

            foreach (var pair in grounds){
                ret.GetFloor(ret.GetStagePosition(pair.position)).GroundType = pair.value;
            }

            var weighted = new WeightedRandomGroup<RandomObjectInfo>();
            foreach (var randomObjectGroup in randomGroups){
                var result = randomObjectGroup.GetResult(weighted);
                var floor = ret.GetFloor(ret.GetStagePosition(result.position));
                floor.TileObjectType = result.objectInfo.type;
                floor.TileObjectId = result.objectInfo.id;
            }

            return ret;
        }
    }
}