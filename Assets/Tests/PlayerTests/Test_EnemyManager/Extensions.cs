using UnityEngine;

namespace Tests.PlayerTests.Test_EnemyManager{
    public static class Extensions{
        public static T CreateGameObjectWithComponent<T>(this MonoBehaviour parent) where T: MonoBehaviour{
            var obj = new GameObject();
            return obj.AddComponent<T>();
        }
    }
}