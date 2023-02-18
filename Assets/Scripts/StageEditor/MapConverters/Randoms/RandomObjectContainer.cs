using System;
using Components.Stages.Templates;
using Components.TileObjects;
using UnityEngine;

namespace StageEditor.MapConverters.Randoms{
    public class RandomObjectContainer: MonoBehaviour{
        public string groupId;
        public float weight;
        public TileObjectInfo info;
        [NonSerialized] public Vector2Int position;

        public RandomObjectInfo ToInfo(){
            return new(){
                objectInfo = info,
                position = position,
                weight = weight
            };
        }
    }
}