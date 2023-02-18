using System.Collections.Generic;
using UnityEngine;

namespace Components.Stages.Templates{
    public class WeightedRandomGroup<T>{
        public struct WeightPair{
            public T Target;
            public float weight;
        }

        private readonly Dictionary<float, T> _range = new();
        private float _totalWeight;

        public WeightPair[] WeightPairs{
            set{
                _range.Clear();
                var cur = 0f;
                foreach (var pair in value){
                    cur += pair.weight;
                    _range[cur] = pair.Target;
                }
                _totalWeight = cur;
            }
        }

        public T GetResult(){
            var randomResult = Random.value * _totalWeight;
            foreach (var pair in _range){
                if (pair.Key > randomResult) return pair.Value;
            }
            return default;
        }
    }

    // public static class WeightedRandom{
    //     public class WeightPair<T>{
    //         public T Target;
    //         public float Weight;
    //     }
    //
    //     public static T GetResult<T>(IEnumerable<WeightPair<T>> input){
    //         
    //     }
    // }
}