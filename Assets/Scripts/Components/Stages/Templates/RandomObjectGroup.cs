using System;
using System.Linq;
using Components.TileObjects;
using MVC;
using UnityEngine;

namespace Components.Stages.Templates{
    
    [Serializable]
    public struct RandomObjectInfo{
        public TileObjectInfo objectInfo;
        public float weight;
        public Vector2Int position;
    }
    
    [Serializable]
    public class RandomObjectGroup: Model{
        public string groupId;
        public RandomObjectInfo[] infos;

        public RandomObjectInfo GetResult(WeightedRandomGroup<RandomObjectInfo> weighted){
            weighted.WeightPairs = infos.Select((info) => new WeightedRandomGroup<RandomObjectInfo>.WeightPair(){
                    Target = info,
                    weight = info.weight
                })
            .ToArray();
            return weighted.GetResult();
        }
    }
}